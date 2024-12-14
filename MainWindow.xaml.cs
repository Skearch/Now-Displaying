using Microsoft.Win32;
using SpotifyAPI.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;
using MessageBox = System.Windows.MessageBox;

namespace NowDisplaying
{
    public partial class MainWindow : Window
    {
        private const string ConfigFolderPath = "Config";
        private string ConfigFilePath => Path.Combine(ConfigFolderPath, "ConfigFile.json");
        private ConfigFile ConfigFile { get; set; }
        private SpotifyClient SpotifyClient { get; set; }

        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;
        private MenuItem showHideMenuItem;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private void InitializeTrayIcon()
        {
            _contextMenu = new ContextMenu();

            var nowDisplayingMenuItem = new MenuItem("NowDisplaying");
            _contextMenu.MenuItems.Add(nowDisplayingMenuItem);

            showHideMenuItem = new MenuItem("Hide", ShowHideMenuItem_Click);
            _contextMenu.MenuItems.Add(showHideMenuItem);

            var closeMenuItem = new MenuItem("Close", CloseMenuItem_Click);
            _contextMenu.MenuItems.Add(closeMenuItem);

            _notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources.NowDisplaying,
                ContextMenu = _contextMenu,
                Visible = false
            };
            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Environment.Exit(1);
        }

        private void ShowHideMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.Hide();
                showHideMenuItem.Text = "Show";
            }
            else
            {
                this.Show();
                this.WindowState = WindowState.Normal;
                showHideMenuItem.Text = "Hide";
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowHideMenuItem_Click(sender, e);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (ConfigFile.Settings.MinimizedOnStart)
                {
                    e.Cancel = true;
                    this.Hide();
                    _notifyIcon.Visible = true;
                    showHideMenuItem.Text = "Show";
                    SaveConfig();
                }
                else
                {
                    SaveConfig();
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error on saving: {ex.Message}");
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!LoadConfig())
                    return;

                await InitializeSpotifyClientAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading or creating config: {ex.Message}");
            }
        }

        private bool LoadConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                ConfigFile = JsonSerializer.Deserialize<ConfigFile>(json);
            }
            else
                CreateDefaultConfig();

            ApplyConfigToUI();
            return true;
        }

        private void CreateDefaultConfig()
        {
            ConfigFile = new ConfigFile
            {
                Spotify = new Spotify
                {
                    RedirectUri = "",
                    ClientId = "",
                    ClientSecret = "",
                    AccessToken = "",
                    TokenExpiration = DateTime.MinValue
                },
                UserInterface = new UserInterface
                {
                    BackgroundBlur = 0
                },
                Settings = new Settings
                {
                    DisplayIndex = 1,
                    StartOnWindows = true,
                    MinimizedOnStart = true
                }
            };

            var json = JsonSerializer.Serialize(ConfigFile, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(ConfigFolderPath);
            File.WriteAllText(ConfigFilePath, json);
        }

        private void ApplyConfigToUI()
        {
            cbMinimizeOnStart.IsChecked = ConfigFile.Settings.MinimizedOnStart;
            cbStartOnWindows.IsChecked = ConfigFile.Settings.StartOnWindows;
            tbRedirectUri.Text = ConfigFile.Spotify.RedirectUri;
            tbClientId.Text = ConfigFile.Spotify.ClientId;
            tbClientSecret.Text = ConfigFile.Spotify.ClientSecret;

            if (ConfigFile.Settings.MinimizedOnStart)
            {
                this.Hide();
                _notifyIcon.Visible = true;
                showHideMenuItem.Text = "Show";
                SaveConfig();
            }

            if (ConfigFile.Settings.StartOnWindows)
                SetStartup(true);
            else
                SetStartup(false);
        }

        private async Task InitializeSpotifyClientAsync()
        {
            if (string.IsNullOrEmpty(ConfigFile.Spotify.AccessToken) || ConfigFile.Spotify.TokenExpiration <= DateTime.Now)
            {
                btnConnectToSpotify.Visibility = Visibility.Visible;
                return;
            }

            SpotifyClient = new SpotifyClient(ConfigFile.Spotify.AccessToken);
            var trackDisplayWindow = new TrackDisplayWindow(SpotifyClient, ConfigFile);
            trackDisplayWindow.Show();
        }

        private void SaveConfig()
        {
            var json = JsonSerializer.Serialize(ConfigFile, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);
        }

        private async Task<AuthorizationCodeTokenResponse> GetAuthorizationCodeTokenAsync()
        {
            var authCode = await GetAuthorizationCodeAsync();
            if (authCode == null)
                return null;

            var tokenResponse = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(ConfigFile.Spotify.ClientId, ConfigFile.Spotify.ClientSecret, authCode, new Uri(ConfigFile.Spotify.RedirectUri))
            );

            return tokenResponse;
        }

        private async Task<string> GetAuthorizationCodeAsync()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add(ConfigFile.Spotify.RedirectUri);
                listener.Start();

                var context = await listener.GetContextAsync();
                var code = context.Request.QueryString["code"];

                var response = context.Response;
                string responseString = "<html><body>You can close this window now and restart Now Displaying.</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();

                return code;
            }
        }

        private async void btnConnectToSpotify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginRequestAsync();

                var authorizationCodeToken = await GetAuthorizationCodeTokenAsync();
                ConfigFile.Spotify.AccessToken = authorizationCodeToken.AccessToken;
                ConfigFile.Spotify.TokenExpiration = DateTime.Now.AddSeconds(authorizationCodeToken.ExpiresIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to Spotify. {ex.Message}");
            }
        }

        private async Task LoginRequestAsync()
        {
            var loginRequest = new LoginRequest(new Uri(ConfigFile.Spotify.RedirectUri),
                ConfigFile.Spotify.ClientId,
                LoginRequest.ResponseType.Code)
            {
                Scope = new[] { Scopes.UserReadCurrentlyPlaying }
            };

            var uri = loginRequest.ToUri();
            await new Utility().OpenBrowserAsync(uri);
        }

        private void cbMinimizeOnStart_Checked(object sender, RoutedEventArgs e) => ConfigFile.Settings.MinimizedOnStart = true;

        private void cbMinimizeOnStart_Unchecked(object sender, RoutedEventArgs e) => ConfigFile.Settings.MinimizedOnStart = false;

        private void cbStartOnWindows_Checked(object sender, RoutedEventArgs e)
        {
            ConfigFile.Settings.StartOnWindows = true;
            SetStartup(true);
        }

        private void cbStartOnWindows_Unchecked(object sender, RoutedEventArgs e)
        {
            ConfigFile.Settings.StartOnWindows = false;
            SetStartup(false);
        }

        private void tbRedirectUri_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ConfigFile.Spotify.RedirectUri = tbRedirectUri.Text;

        private void tbClientId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ConfigFile.Spotify.ClientId = tbClientId.Text;

        private void tbClientSecret_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ConfigFile.Spotify.ClientSecret = tbClientSecret.Text;

        private void SetStartup(bool enable)
        {
            const string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(runKey, true))
            {
                if (key == null)
                {
                    MessageBox.Show("Failed to open registry key for startup settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (enable)
                {
                    var existingValue = key.GetValue("NowDisplaying");
                    var executablePath = $"\"{System.Reflection.Assembly.GetExecutingAssembly().Location}\"";

                    if (existingValue == null || !existingValue.Equals(executablePath))
                        key.SetValue("NowDisplaying", executablePath);
                }
                else
                    key.DeleteValue("NowDisplaying", false);
            }
        }
    }
}