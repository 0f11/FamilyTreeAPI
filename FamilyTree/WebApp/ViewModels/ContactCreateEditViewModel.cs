using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ContactCreateEditViewModel
    {
        public Contact Contact { get; set; } = default!;

        public SelectList? PersonsSelectList { get; set; }
        public SelectList? ContactTypesSelectList { get; set; }

    }
}