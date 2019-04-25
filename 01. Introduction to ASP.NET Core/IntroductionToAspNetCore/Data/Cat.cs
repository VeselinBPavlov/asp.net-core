namespace IntroductionToAspNetCore.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        [Range(0, 30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(2000), MinLength(10)]
        public string ImageUrl { get; set; }
    }
}
