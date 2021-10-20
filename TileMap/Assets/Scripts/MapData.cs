using System;
using System.Collections.Generic;

public class MapData
{
    public List<Layer> layers { get; set; }
    public class Layer
    {
        public List<int> data { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }
}