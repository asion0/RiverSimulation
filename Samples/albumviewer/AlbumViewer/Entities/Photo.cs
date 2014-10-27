using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlbumViewer
{
    public struct Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}
