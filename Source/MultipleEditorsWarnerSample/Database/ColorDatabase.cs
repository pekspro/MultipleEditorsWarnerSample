using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;

namespace MultipleEditorsWarnerSample.Database
{
    public class ColorDatabase
    {
        public static ColorDatabase Instance = new ColorDatabase();

        public ColorDatabase()
        {
            Colors.AddRange(from KnownColor knownColor in Enum.GetValues(typeof(KnownColor))
                            let color = Color.FromKnownColor(knownColor)
                            where color.IsSystemColor == false
                            select new ColorEntity()
                            {
                                ColorID = Colors.Count,
                                Name = color.Name,
                                R = color.R,
                                G = color.G,
                                B = color.B
                            });
        }

        public List<ColorEntity> Colors { get; set; } = new List<ColorEntity>();
    }

    public class ColorEntity
    {
        public int ColorID { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
                
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }
    }
}
