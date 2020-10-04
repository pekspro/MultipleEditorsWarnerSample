using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultipleEditorsWarnerSample.Database;

namespace MultipleEditorsWarnerSample.Pages
{
    public class EditModel : PageModel
    {
        [FromForm]
        public ColorEntity Color { get; set; } = null!;

        [FromQuery]
        public string? UserName { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(UserName == null)
            {
                return Redirect("/");
            }

            Color = ColorDatabase.Instance.Colors[id.Value];

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            ColorDatabase.Instance.Colors[Color.ColorID] = Color;

            return Redirect("/?UserName=" + Uri.EscapeDataString(UserName!));
        }
    }
}
