﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ExtryzeDLL.Blam;
using ExtryzeDLL.Blam.ThirdGen.Structures;

namespace Assembly.Metro.Controls.PageTemplates.Games
{
    public class TagHierarchy
    {
        public ObservableCollection<TagClass> Classes { get; set; }
        public List<TagEntry> Entries { get; set; }
    }

    public class TagClass
    {
        public TagClass(ITagClass baseClass, string name)
        {
            RawClass = baseClass;
            TagClassMagic = name;
            Children = new List<TagEntry>();
        }

        public string TagClassMagic { get; set; }
        public ITagClass RawClass { get; set; }
        public List<TagEntry> Children { get; private set; }
    }

    public class TagEntry
    {
        public TagEntry(ITag baseTag, TagClass parent, string name)
        {
            RawTag = baseTag;
            TagFileName = name;
            ParentClass = parent;
        }

        public string TagFileName { get; private set; }
        public ITag RawTag { get; private set; }
        public TagClass ParentClass { get; private set; }
    }
}
