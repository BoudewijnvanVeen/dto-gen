using System;
using System.Collections.Generic;
using System.Linq;

namespace DtoGen.Core.Generators
{
    /// <summary>
    /// Represents a property member.
    /// </summary>
    public class PropertyMember
    {

        /// <summary>
        /// Gets the type of the property in the source class.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the name of the property in the source class.
        /// </summary>
        public Type Type { get; set; }


        /// <summary>
        /// Gets the type of the property in the target class.
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// Gets the name of the property in the target class.
        /// </summary>
        public string TargetName { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMember"/> class.
        /// </summary>
        public PropertyMember()
        {
            PropertyMemberRenderer = new AssignmentPropertyMemberRenderer(this);
        }



        /// <summary>
        /// Gets or sets the instance that is responsible for rendering of this member.
        /// </summary>
        public IPropertyMemberRenderer PropertyMemberRenderer { get; set; }

    }
}