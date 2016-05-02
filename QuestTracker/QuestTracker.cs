﻿using System.ComponentModel.Composition;
using Grabacr07.KanColleViewer.Composition;
using QuestTracker.ViewModels;
using QuestTracker.Views;
using Grabacr07.KanColleWrapper;

namespace QuestTracker
{
    [Export(typeof (IPlugin))]
    [Export(typeof (ITool))]
    [ExportMetadata("Guid", "73584674-DC71-4CE2-AEF6-29F2DF8A41E6")]
    [ExportMetadata("Title", "QuestTracker")]
    [ExportMetadata("Description", "任務の進行度を追跡する。")]
    [ExportMetadata("Version", "1.1.4")]
    [ExportMetadata("Author", "+PaddyXu")]
    public class QuestTracker : IPlugin, ITool
    {
        private PortalViewModel viewerViewModel;

        public void Initialize()
        {
            viewerViewModel = new PortalViewModel(KanColleClient.Current);
        }

        string ITool.Name => "QTrack";

        object ITool.View => new Portal {DataContext = viewerViewModel};
    }
}