using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AlbumViewer
{
    public struct Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ReadOnlyCollection<Photo> Photos { get; set; }
    }
}
