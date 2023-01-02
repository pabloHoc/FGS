using System;
using System.Collections.Generic;
using System.Reflection;
using Myra.Graphics2D.UI;
using FSG.Core;
using Myra.Assets;

namespace FSG.UI
{
	public class TabsController : UIController
	{
        private readonly List<string> _tabs;
        private readonly List<Widget> _tabItems;
        private readonly Panel _tabItemsContainer;

        public TabsController(
            ServiceProvider serviceProvider,
            UIEventManager eventManager,
            AssetManager assetManager,
            Dictionary<string, Widget> tabs
        ) : base("../../../UI/Common/Tabs/Tabs.xaml", serviceProvider, eventManager, assetManager)
        {
            var tabsContainer = (HorizontalStackPanel)Root.FindWidgetById("Tabs");
            _tabItemsContainer = (Panel)Root.FindWidgetById("TabItems");

            tabsContainer.Spacing = 8;

            var tabNumber = tabs.Keys.Count;
            _tabs = new List<string>(tabs.Keys);
            _tabItems = new List<Widget>(tabs.Values);

            foreach (var entry in tabs)
            {
                var tab = new TextButton
                {
                    Id = entry.Key,
                    Text = entry.Key
                };
                tab.Click += HandleTabClick;
                tabsContainer.Widgets.Add(tab);
            }

            _tabItemsContainer.Widgets.Add(_tabItems[0]);
        }

        private void HandleTabClick(object sender, EventArgs e)
        {
            SwitchToTab(_tabs.IndexOf(((TextButton)sender).Id));
        }

        private void SwitchToTab(int tabIndex)
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                if (i == tabIndex)
                {
                    if (!_tabItemsContainer.Widgets.Contains(_tabItems[i]))
                    {
                        _tabItemsContainer.Widgets.Add(_tabItems[i]);
                    }
                }
                else
                {
                    if (_tabItemsContainer.Widgets.Contains(_tabItems[i]))
                    {
                        _tabItemsContainer.Widgets.Remove(_tabItems[i]);
                    }
                }
            }
        }

        public void SwitchTo(string tabName)
        {
            SwitchToTab(_tabs.IndexOf(tabName));
        }
    }
}

