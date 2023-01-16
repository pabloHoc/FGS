using System;
using System.Collections.Generic;

namespace FSG.Entities
{
	// TODO: check where to put it
	public class District
	{
		public string Name { get; init; }

		public List<string> Buildings { get; } = new List<string>();

		public District()
		{
		}
	}
}

