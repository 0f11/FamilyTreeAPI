using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class RelationshipsCreateEditViewModel
    {
        public Relationships Relationships { get; set; } = default!;

        public SelectList? FamiliesSelectList { get; set; }
        public SelectList? Persons1SelectList { get; set; }
        public SelectList? Persons2SelectList { get; set; }
        public SelectList? RelationshipTypesSelectList { get; set; }
        public SelectList? RelationshipRoles1SelectList { get; set; }
        public SelectList? RelationshipRoles2SelectList { get; set; }
    }
}