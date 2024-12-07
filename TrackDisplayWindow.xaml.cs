using SpotifyAPI.Web;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace NowDisplaying
{
    public partial class TrackDisplayWindow : Window
    {
        private readonly SpotifyClient _spotifyClient;
        private readonly ConfigFile _configFile;
        private DisplayMedia LastMedia;

        public TrackDisplayWindow(SpotifyClient spotifyClient, ConfigFile configFile)
        {
            InitializeComponent();

            _spotifyClient = spotifyClient ?? throw new ArgumentNullException(nameof(spotifyClient));
            _configFile = configFile ?? throw new ArgumentNullException(nameof(configFile));

            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SetWindowToDisplay(_configFile.DisplayIndex);
                StartBackgroundAnimation();
                _ = PlaybackWatcher();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during window load: {ex.Message}");
            }
        }

        private void SetWindowToDisplay(int displayIndex)
        {
            try
            {
                var screen = Screen.AllScreens.FirstOrDefault(s => s.DeviceName.Contains($"DISPLAY{displayIndex}"));
                if (screen != null)
                {
                    var workingArea = screen.WorkingArea;

                    Left = workingArea.Left;
                    Top = workingArea.Top;
                    Width = workingArea.Width;
                    Height = workingArea.Height;
                }

                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Maximized;
                WindowStartupLocation = WindowStartupLocation.Manual;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting window to display: {ex.Message}");
            }
        }

        private void StartBackgroundAnimation()
        {
            try
            {
                var random = new Random();

                var xAnimation = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(20),
                    RepeatBehavior = RepeatBehavior.Forever,
                    AutoReverse = true
                };

                var yAnimation = new DoubleAnimationUsingKeyFrames
                {
                    Duration = TimeSpan.FromSeconds(20),
                    RepeatBehavior = RepeatBehavior.Forever,
                    AutoReverse = true
                };

                var easingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

                for (double i = 0; i <= 1; i += 0.25)
                {
                    int xValue = random.Next(-50, 50);
                    int yValue = random.Next(-50, 50);

                    xAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(xValue, KeyTime.FromPercent(i), easingFunction));
                    yAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(yValue, KeyTime.FromPercent(i), easingFunction));
                }

                backgroundImageTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
                backgroundImageTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error starting background animation: {ex.Message}");
            }
        }

        private async Task PlaybackWatcher()
        {
            while (true)
            {
                try
                {
                    var currentlyPlaying = await _spotifyClient.Player.GetCurrentlyPlaying(new PlayerCurrentlyPlayingRequest());

                    switch (currentlyPlaying.Item)
                    {
                        case FullTrack fullTrack:
                            var displayFullTrack = new DisplayMedia()
                            {
                                Id = fullTrack.Id,
                                Name = fullTrack.Name,
                                Artists = fullTrack.Artists.Select(a => a.Name).ToArray(),
                                AlbumArtUrl = fullTrack.Album.Images.FirstOrDefault()?.Url
                            };

                            UpdateTrackInfo(displayFullTrack);
                            break;
                        case FullEpisode episode:
                            var displayEpisode = new DisplayMedia()
                            {
                                Id = episode.Id,
                                Name = episode.Name,
                                Artists = new string[] { episode.Show.Publisher },
                                AlbumArtUrl = episode.Show.Images.FirstOrDefault()?.Url
                            };

                            UpdateTrackInfo(displayEpisode);
                            break;
                        default:
                            Debug.WriteLine("No track is currently playing.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in PlaybackWatcher: {ex.Message}");
                }

                await Task.Delay(5000);
            }
        }

        private void UpdateTrackInfo(DisplayMedia media)
        {
            try
            {
                if (media == null)
                {
                    LastMedia = media;
                    return;
                }

                if (LastMedia != null && LastMedia.Id.Equals(media.Id))
                    return;

                LastMedia = media;

                Debug.WriteLine($"Currently playing: {media.Name} by {string.Join(", ", media.Artists)}");

                var albumCoverUrl = media.AlbumArtUrl;
                if (albumCoverUrl == null)
                    return;

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(albumCoverUrl);
                bitmap.EndInit();

                var scaleTransform = new ScaleTransform(1, 1);
                pbAlbumCover.RenderTransform = scaleTransform;
                pbAlbumCover.RenderTransformOrigin = new Point(0, 1);

                var scaleUpStoryboard = new Storyboard();
                var scaleUpXAnimation = new DoubleAnimation(1.8, TimeSpan.FromSeconds(0.7));
                var scaleUpYAnimation = new DoubleAnimation(1.8, TimeSpan.FromSeconds(0.7));
                Storyboard.SetTarget(scaleUpXAnimation, pbAlbumCover);
                Storyboard.SetTarget(scaleUpYAnimation, pbAlbumCover);
                Storyboard.SetTargetProperty(scaleUpXAnimation, new PropertyPath("RenderTransform.ScaleX"));
                Storyboard.SetTargetProperty(scaleUpYAnimation, new PropertyPath("RenderTransform.ScaleY"));
                scaleUpStoryboard.Children.Add(scaleUpXAnimation);
                scaleUpStoryboard.Children.Add(scaleUpYAnimation);

                var pbAlbumCoverFadeOutStoryboard = new Storyboard();
                var pbAlbumCoverFadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
                Storyboard.SetTarget(pbAlbumCoverFadeOutAnimation, pbAlbumCover);
                Storyboard.SetTargetProperty(pbAlbumCoverFadeOutAnimation, new PropertyPath(OpacityProperty));
                pbAlbumCoverFadeOutStoryboard.Children.Add(pbAlbumCoverFadeOutAnimation);

                var fadeOutStoryboard = new Storyboard();
                var fadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
                Storyboard.SetTarget(fadeOutAnimation, pbAlbumCoverBackground);
                Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(OpacityProperty));
                fadeOutStoryboard.Children.Add(fadeOutAnimation);

                fadeOutStoryboard.Completed += (s, e) =>
                {
                    pbAlbumCoverBackground.Source = bitmap;

                    lblSongName.Text = media.Name;
                    lblSongArtists.Text = string.Join(", ", media.Artists);

                    var fadeInBackgroundStoryboard = new Storyboard();
                    var fadeInBackgroundAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
                    Storyboard.SetTarget(fadeInBackgroundAnimation, pbAlbumCoverBackground);
                    Storyboard.SetTargetProperty(fadeInBackgroundAnimation, new PropertyPath(OpacityProperty));
                    fadeInBackgroundStoryboard.Children.Add(fadeInBackgroundAnimation);
                    fadeInBackgroundStoryboard.Begin();
                };

                pbAlbumCoverFadeOutStoryboard.Completed += (s, e) =>
                {
                    pbAlbumCover.Source = bitmap;

                    var fadeInBackgroundStoryboard = new Storyboard();
                    var fadeInBackgroundAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
                    Storyboard.SetTarget(fadeInBackgroundAnimation, pbAlbumCover);
                    Storyboard.SetTargetProperty(fadeInBackgroundAnimation, new PropertyPath(OpacityProperty));
                    fadeInBackgroundStoryboard.Children.Add(fadeInBackgroundAnimation);
                    fadeInBackgroundStoryboard.Begin();
                };

                scaleUpStoryboard.Completed += (s, e) =>
                {
                    pbAlbumCoverFadeOutStoryboard.Begin();
                };

                pbAlbumCoverFadeOutStoryboard.Completed += (s, e) =>
                {
                    var scaleDownStoryboard = new Storyboard();
                    var scaleDownXAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.7)) { BeginTime = TimeSpan.FromSeconds(1) };
                    var scaleDownYAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.7)) { BeginTime = TimeSpan.FromSeconds(1) };
                    Storyboard.SetTarget(scaleDownXAnimation, pbAlbumCover);
                    Storyboard.SetTarget(scaleDownYAnimation, pbAlbumCover);
                    Storyboard.SetTargetProperty(scaleDownXAnimation, new PropertyPath("RenderTransform.ScaleX"));
                    Storyboard.SetTargetProperty(scaleDownYAnimation, new PropertyPath("RenderTransform.ScaleY"));
                    scaleDownStoryboard.Children.Add(scaleDownXAnimation);
                    scaleDownStoryboard.Children.Add(scaleDownYAnimation);
                    scaleDownStoryboard.Begin();
                    fadeOutStoryboard.Begin();
                };

                scaleUpStoryboard.Begin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating track info: {ex.Message}");
            }
        }
    }
}