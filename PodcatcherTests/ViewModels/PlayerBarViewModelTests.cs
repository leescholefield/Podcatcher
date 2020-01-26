using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Podcatcher.ViewModels.Services;

namespace Podcatcher.ViewModels.Tests
{
    [TestClass()]
    public class PlayerBarViewModelTests
    {
        private PlayerBarViewModel ViewModel;
        private Mock<IPlaybackService> MockedService;

        [TestInitialize()]
        public void SetupTest()
        {
            MockedService = new Mock<IPlaybackService>();
            ViewModel = new PlayerBarViewModel(MockedService.Object);
        }

        #region Commands

        [TestMethod()]
        public void TogglePlayback_Calls_PlaybackService_Play_When_IsPlaying_Is_False()
        {
            ViewModel.IsPlaying = false;
            ViewModel.TogglePlayback.Execute(this); // execute param ignored in imp

            MockedService.Verify(m => m.Play());
        }

        [TestMethod()]
        public void TogglePlayback_Calls_PlaybackService_Pause_When_IsPlaying_Is_True()
        {
            ViewModel.IsPlaying = true;
            ViewModel.TogglePlayback.Execute(this); // execute param ignored in imp.

            MockedService.Verify(m => m.Pause());
        }

        [TestMethod()]
        public void SkipForward_Calls_PlaybackService_SkipForward()
        {
            ViewModel.SkipForward.Execute(this); // execute param ignored in imp

            MockedService.Verify(m => m.SkipForward(It.IsAny<int>()));
        }

        [TestMethod()]
        public void SkipBack_Calls_PlaybackService_SkipBack()
        {
            ViewModel.SkipBack.Execute(this); // execute param ignored in imp

            MockedService.Verify(m => m.SkipBack(It.IsAny<int>()));
        }

        #endregion

        #region Subscibes to PlaybackService events

        [TestMethod()]
        public void PlaybackStateChanged_Event_Sets_IsPlaying_To_True_When_State_Playing()
        {
            MockedService.Raise(e => e.PlaybackStateChanged += null, new PlaybackStateChangedEventArgs(PlaybackState.PLAYING));

            Assert.IsTrue(ViewModel.IsPlaying);
        }

        [TestMethod()]
        public void PlaybackStateChanged_Event_Sets_IsPlaying_To_False_When_State_Paused()
        {
            ViewModel.IsPlaying = true;

            MockedService.Raise(e => e.PlaybackStateChanged += null, new PlaybackStateChangedEventArgs(PlaybackState.PAUSED));

            Assert.IsFalse(ViewModel.IsPlaying);
        }

        [TestMethod()]
        public void PlaybackStateChanged_Event_Sets_IsPlaying_To_False_When_State_Stopped()
        {
            ViewModel.IsPlaying = true;

            MockedService.Raise(e => e.PlaybackStateChanged += null, new PlaybackStateChangedEventArgs(PlaybackState.STOPPED));

            Assert.IsFalse(ViewModel.IsPlaying);
        }

        [TestMethod()]
        public void ViewModel_Subscribes_To_NowPlayingChanged_Event_In_PlaybackService()
        {
            ViewModel.Playing = null;

            MockedService.Raise(e => e.NowPlayingChanged += null, new MediaItemChangedEventArgs(new Models.Playback.MediaItem()));

            Assert.IsNotNull(ViewModel.Playing);
        }

        #endregion

    }
}