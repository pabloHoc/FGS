﻿using System.IO;
using FSG.Core;
using FSG.Commands;
using FSG.Entities;
using Myra.Assets;
using Myra.Graphics2D.UI;
using System;

namespace FSG.UI
{
    public class PopulationInfoController : UIController
    {
        private readonly VerticalStackPanel _popList;

        public PopulationInfoController(ServiceProvider serviceProvider, UIEventManager eventManager,AssetManager assetManager)
            : base("../../../UI/InfoPanel/PopulationInfo/PopulationInfo.xaml", serviceProvider, eventManager, assetManager)
        {
            _popList = (VerticalStackPanel)Root.FindWidgetById("PopList");

            _eventManager.OnRegionSelected += HandleRegionSelected;
        }

        private void HandleRegionSelected(object sender, Region region)
        {
            Update();
        }

        private void UpdatePops(Region region)
        {
            _popList.Widgets.Clear();

            foreach (var pop in region.Pops)
            {
                _popList.Widgets.Add(new Label
                {
                    Text = $"{pop.Strata} ({pop.Size}) - GP: {pop.GrowthPoints}"
                });
            }
        }

        private void Clear()
        {
            _popList.Widgets.Clear();
        }

        public override void Update()
        {
            if (_eventManager.SelectedRegion?.Empire != null)
            {
                UpdatePops(_eventManager.SelectedRegion);
            } else
            {
                Clear();
            }
        }
    }
}
