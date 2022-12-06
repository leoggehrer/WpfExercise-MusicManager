﻿namespace Repository.Logic.Models
{
    public class Album : ModelObject, ICloneable
    {
        public string Title { get; set; } = string.Empty;
        public string Interpreter { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Title} {Interpreter}";
        }
        public object Clone()
        {
            return new Album
            {
                Id = Id,
                Title = Title,
                Interpreter = Interpreter,
            };
        }
    }
}
