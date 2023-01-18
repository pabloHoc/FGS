using System;
using System.Collections.Generic;

namespace FSG.Entities
{
	// Check where to put it
	public class Capital
	{
		public List<District> Districts { get; } = new List<District>();

		public List<string> Buildings { get; } = new List<string>();
	}
}

