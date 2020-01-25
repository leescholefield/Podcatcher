using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Podcatcher.Models;
using Podcatcher.Models.Playback;
using Podcatcher.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.ViewModels.Services.Tests
{
    [TestClass()]
    public class PlaybackServiceTests
    {

        private IPlaybackService Service;
        private Mock<IPlayer> MockedPlayer;

        private readonly Episode TestEpisode = new Episode { StreamUrl = "stream url value" };

        [TestInitialize()]
        public void SetupTest()
        {
            MockedPlayer = new Mock<IPlayer>();
            Service = new PlaybackService(MockedPlayer.Object);
        }

        #region Load

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Load_Throws_Exception_When_Episode_Has_No_StreamUrl()
        {
            Service.Load(new Episode());
        }

        [TestMethod()]
        public void Load_Calls_Player_Play_When_beginPlaybackOnLoaded_Is_True()
        {
            Service.Load(TestEpisode, true);

            MockedPlayer.Verify(m => m.Play());
        }

        #endregion

        #region Play

        [TestMethod()]
        public void Play_Calls_IPlayer_Play_After_Load_Called()
        {
            Service.Load(TestEpisode, false);
            Service.Play();

            MockedPlayer.Verify(m => m.Play());
        }

        [TestMethod()]
        public void Play_Does_Not_Call_IPlayer_If_Load_Not_Called()
        {
            Service.Play();
            MockedPlayer.Verify(m => m.Play(), Times.Never);
        }

        #endregion

    }
}