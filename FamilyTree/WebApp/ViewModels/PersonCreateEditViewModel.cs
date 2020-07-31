using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PersonCreateEditViewModel
    {
        public Person Person { get; set; } = default!;

        public SelectList? GendersSelectList { get; set; }
        public SelectList? ContactTypesSelectList { get; set; }
    }
}