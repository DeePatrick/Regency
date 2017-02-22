using Regency.Models;
using System.Collections.Generic;

namespace Regency.ViewModels
{
    public class RandomActorViewModel
    {
        public Actor Actor { get; set; }
        public List<Customer> Customers { get; set; }
    }
}