using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attributes;

/// <summary>
/// Atributo para ignorar a propriedade na hora do insert
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class IgnorarInsert : Attribute { }
