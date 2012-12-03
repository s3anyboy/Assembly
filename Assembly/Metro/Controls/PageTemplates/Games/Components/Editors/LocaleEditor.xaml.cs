﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assembly.Metro.Dialogs;
using Assembly.Windows;
using ExtryzeDLL.Blam;
using ExtryzeDLL.Blam.ThirdGen;
using ExtryzeDLL.IO;

namespace Assembly.Metro.Controls.PageTemplates.Games.Components.Editors
{
    /// <summary>
    /// Interaction logic for LocaleEditor.xaml
    /// </summary>
    public partial class LocaleEditor : UserControl
    {
        private ThirdGenCacheFile _cache;
        private IReader _reader;
        private ILanguage _currentLanguage;
        private IList<string> _currentLocaleTable;
        private ObservableCollection<LocaleEntry> locales;
        private ObservableCollection<LanguageEntry> languages = new ObservableCollection<LanguageEntry>();

        public LocaleEditor(ThirdGenCacheFile cache, IReader reader, int index)
        {
            InitializeComponent();

            _cache = cache;
            _reader = reader;
            _currentLanguage = cache.Languages[index];

            Thread thrd = new Thread(new ThreadStart(LoadLanguage));
            thrd.SetApartmentState(ApartmentState.STA);
            thrd.Start();
        }

        private void lvLocales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //LocaleEntry localeEntry = (LocaleEntry)localeEntries.SelectedItem;
           //if (localeEntry != null)
           //     _haloMap.txtLocaleSelectedContent.Text = localeEntry.Locale;
        }

        /// <summary>
        /// Load a language into the listview
        /// </summary>
        private void LoadLanguage()
        {
            _currentLocaleTable = _currentLanguage.LoadStrings(_reader);
            locales = new ObservableCollection<LocaleEntry>();

            for (int i = 0; i < _currentLocaleTable.Count; i++)
                locales.Add(new LocaleEntry() { Index = i, Locale = _currentLocaleTable[i] });

            Dispatcher.Invoke(new Action( delegate { lvLocales.DataContext = locales; }));
        }
        /// <summary>
        /// Filter the currently selected language
        /// </summary>
        /// <param name="filter">The filter string</param>
        private void FilterLanguage(string filter)
        {
            locales = new ObservableCollection<LocaleEntry>();
            string searchFilter = filter.ToLower().Trim();

            for (int i = 0; i < _currentLocaleTable.Count; i++)
                if (_currentLocaleTable[i].ToLower().Trim().Contains(searchFilter))
                    locales.Add(new LocaleEntry() { Index = i, Locale = _currentLocaleTable[i] });

            Dispatcher.Invoke(new Action(delegate { lvLocales.DataContext = locales; }));
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
                FilterLanguage(txtFilter.Text);
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterLanguage(txtFilter.Text);
        }
        private void btnSaveAll_Click(object sender, RoutedEventArgs e)
        {
            MetroMessageBox.Show("Not implemented yet, fool.");
        }
    }

    public class LocaleEntry
    {
        public int Index { get; set; }
        public string Locale { get; set; }
    }
    public class LanguageEntry
    {
        public int Index { get; set; }
        public string Language { get; set; }
    }
}
