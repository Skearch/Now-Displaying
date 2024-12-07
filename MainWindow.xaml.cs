using SpotifyAPI.Web;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NowDisplaying
{
    public partial class MainWindow : Window
    {
        private const string ConfigFolderPath = "Config";
        private string ConfigFilePath = Path.Combine(ConfigFolderPath, "ConfigFile.json");
        private ConfigFile configFile { get; set; }
        private SpotifyClient SpotifyClient { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadOrCreateConfig();
            Loaded += MainWindow_Loaded;
        }

        private void LoadOrCreateConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    configFile = JsonSerializer.Deserialize<ConfigFile>(json);
                }
                else
                {
                    configFile = new ConfigFile
                    {
                        RedirectUri = "",
                        ClientId = "",
                        ClientSecret = "",
                        DisplayIndex = 0,
                        AccessToken = "",
                        TokenExpiration = DateTime.MinValue
                    };
                    var json = JsonSerializer.Serialize(configFile, new JsonSerializerOptions { WriteIndented = true });
                    Directory.CreateDirectory(ConfigFolderPath);
                    File.WriteAllText(ConfigFilePath, json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading or creating config: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(configFile.AccessToken) || configFile.TokenExpiration <= DateTime.Now)
                {
                    var authCode = await GetAuthorizationCodeAsync();
                    if (authCode == null) return;

                    var tokenResponse = await new OAuthClient().RequestToken(
                        new AuthorizationCodeTokenRequest(configFile.ClientId, configFile.ClientSecret, authCode, new Uri(configFile.RedirectUri))
                    );

                    configFile.AccessToken = tokenResponse.AccessToken;
                    configFile.TokenExpiration = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
                    SaveConfig();
                }

                SpotifyClient = new SpotifyClient(configFile.AccessToken);

                var trackDisplayWindow = new TrackDisplayWindow(this.SpotifyClient, configFile);
                trackDisplayWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during window load: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private void SaveConfig()
        {
            try
            {
                var json = JsonSerializer.Serialize(configFile, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving config: {ex.Message}");
            }
        }

        private async Task<string> GetAuthorizationCodeAsync()
        {
            try
            {
                using (var listener = new HttpListener())
                {
                    listener.Prefixes.Add(configFile.RedirectUri);
                    listener.Start();

                    var context = await listener.GetContextAsync();
                    var code = context.Request.QueryString["code"];

                    var response = context.Response;
                    string responseString = "<html><body>You can close this window now.</body></html>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    response.OutputStream.Close();

                    return code;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting authorization code: {ex.Message}");
                Environment.Exit(1);
            }

            return null;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginRequest = new LoginRequest(new Uri(configFile.RedirectUri), configFile.ClientId, LoginRequest.ResponseType.Code)
                {
                    Scope = new[] { Scopes.UserReadCurrentlyPlaying }
                };

                var uri = loginRequest.ToUri();
                await new Utility().OpenBrowserAsync(uri);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login: {ex.Message}");
            }
        }
    }
}