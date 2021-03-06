﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tais.API;

namespace Tais.Mod
{
    class Modder
    {
        internal Dictionary<string, ModItem> modItems;

        internal IEnumerable<EventDef> events;

        internal Dictionary<string, IEnumerable<Language>> languages;

        internal Dictionary<string, PersonName> personName;

        internal IEnumerable<InitSelect> initSelects;

        internal IEnumerable<DepartDef> departs;

        internal IEnumerable<AdjustDef> adjusts;

        internal ChaotingDef chaoting;

        internal static Modder Load(string modPath)
        {
            var modder = new Modder();

            foreach (var subpath in Directory.EnumerateDirectories(modPath))
            {
                modder.modItems.Add(Path.GetFileName(subpath), new ModItem(subpath));
            }

            modder.Init();

            return modder;
        }

        internal Modder()
        {
            modItems = new Dictionary<string, ModItem>();
        }

        private void Init()
        {
            events = modItems.SelectMany(y=>y.Value.events);
            languages = modItems.ToDictionary(x => x.Key, y => y.Value.languages);

            personName = modItems.SelectMany(x => x.Value.languages).ToDictionary(x => x.locale, y => y.personName);
            departs = modItems.SelectMany(x => x.Value.departs);

            if (modItems.Where(x=>x.Key != "Native" && x.Value.initSelects.Any()).Count() > 1)
            {
                throw new Exception();
            }

            initSelects = modItems.SelectMany(x => x.Value.initSelects);

            adjusts = modItems.SelectMany(x => x.Value.adjusts);

            chaoting = modItems.First().Value.chaoting;
        }
    }
}
