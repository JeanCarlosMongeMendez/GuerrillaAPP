using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuerrillaAPI.Model;
using GuerrillaAPI.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace GuerrillaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class guerrillaController : ControllerBase
    {

        #region global variables
        private readonly GuerrillaAPPContext _context;
        private readonly string _oil = "oil";
        private readonly string _money = "money";
        private readonly string _people = "people";
        private readonly string _bunker = "bunker";
        private readonly string _assault = "assault";
        private readonly string _engineer = "engineer";
        private readonly string _tank = "tank";
        #endregion

        public guerrillaController(GuerrillaAPPContext context)
        {
            _context = context;
        }

        //[EnableCors("GetAllPolicy")]
        [HttpGet]
        public string GetAllGuerrillas(string faction = null, string email = null, string name = null)
        {
            try
            {
                List<Guerrilla> guerrillas = new List<Guerrilla>();

                if (String.IsNullOrEmpty(faction) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(name))
                    guerrillas = _context.Guerrilla.ToList();
                if (String.IsNullOrEmpty(faction) && String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(name))
                    guerrillas = _context.Guerrilla.Where(g => g.Name.Equals(name)).ToList();
                if (String.IsNullOrEmpty(faction) && !String.IsNullOrEmpty(email) && String.IsNullOrEmpty(name))
                    guerrillas = _context.Guerrilla.Where(g => g.Email.Equals(email)).ToList();
                if (!String.IsNullOrEmpty(faction) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(name))
                    guerrillas = _context.Guerrilla.Where(g => g.Faction.Equals(faction)).ToList();

                List<guerrillas> guerrillasDTO = new List<guerrillas>();
                
                foreach(Guerrilla guerrilla in guerrillas)
                {
                    guerrillasDTO.Add(new guerrillas { guerrillaName = guerrilla.Name, faction = guerrilla.Faction, rank = guerrilla.Rank });
                }
                return JsonConvert.SerializeObject(guerrillasDTO);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{name}")]
        public string GetGuerrilla(string name)
        {
            try
            {
                return GetGuerrillaDTO(name);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("{name}")]
        public string CreateGuerrilla(guerrilla guerrillaDTO, string name)
        {
            try
            {
                Guerrilla guerrilla = new Guerrilla();
                guerrilla.Name = guerrillaDTO.guerrillaName;
                guerrilla.Email = guerrillaDTO.email;
                guerrilla.Faction = guerrillaDTO.faction;
                guerrilla.Rank = 0;
                guerrilla.Timestamp = 0;
                _context.Guerrilla.Add(guerrilla);
                GuerrillaResources oil = new GuerrillaResources { Guerrilla = guerrilla.Name, Quantity = 0, Resource = _oil };
                GuerrillaResources money = new GuerrillaResources { Guerrilla = guerrilla.Name, Quantity = 0, Resource = _money };
                GuerrillaResources people = new GuerrillaResources { Guerrilla = guerrilla.Name, Quantity = 0, Resource = _people };
                _context.GuerrillaResources.Add(oil);
                _context.GuerrillaResources.Add(money);
                _context.GuerrillaResources.Add(people);

                GuerrillaUnits bunker = new GuerrillaUnits { Guerrilla = guerrilla.Name, Quantity = 0, Unit = _bunker };
                GuerrillaUnits assault = new GuerrillaUnits { Guerrilla = guerrilla.Name, Quantity = 0, Unit = _assault };
                GuerrillaUnits engineer = new GuerrillaUnits { Guerrilla = guerrilla.Name, Quantity = 0, Unit = _engineer };
                GuerrillaUnits tank = new GuerrillaUnits { Guerrilla = guerrilla.Name, Quantity = 0, Unit = _tank };
                _context.GuerrillaUnits.Add(bunker);
                _context.GuerrillaUnits.Add(assault);
                _context.GuerrillaUnits.Add(engineer);
                _context.GuerrillaUnits.Add(tank);
                _context.SaveChanges();

                return GetGuerrillaDTO(guerrilla.Name);
            }
            catch
            {
                throw;
            }
        }

        private string GetGuerrillaDTO(string name)
        {
            //load guerrilla
            var guerrilla = (from guerrillaDB in _context.Guerrilla where guerrillaDB.Name.Equals(name) 
                             select new guerrilla()
                               {
                                   guerrillaName = guerrillaDB.Name,
                                   name = guerrillaDB.Name,
                                   email = guerrillaDB.Email,
                                   faction = guerrillaDB.Faction,
                                   rank = guerrillaDB.Rank,
                                   timestamp = guerrillaDB.Timestamp
                               }).Single();

            //load resources
            resources resources = new resources();
            List<GuerrillaResources> resourcesDB = _context.GuerrillaResources.Where(r => r.Guerrilla.Equals(name)).ToList();
            foreach(GuerrillaResources guerrillaResources in resourcesDB)
            {
                if (guerrillaResources.Resource.Equals(_oil))
                    resources.oil = guerrillaResources.Quantity;
                if (guerrillaResources.Resource.Equals(_money))
                    resources.money = guerrillaResources.Quantity;
                if (guerrillaResources.Resource.Equals(_people))
                    resources.people = guerrillaResources.Quantity;
            }
            guerrilla.resources = resources;

            //load units
            buildings buildings = new buildings();
            army army = new army();
            List<GuerrillaUnits> unitsDB = _context.GuerrillaUnits.Where(u => u.Guerrilla.Equals(name)).ToList();
            foreach (GuerrillaUnits guerrillaUnits in unitsDB)
            {
                if (guerrillaUnits.Unit.Equals(_bunker))
                    buildings.bunker = guerrillaUnits.Quantity;
                if (guerrillaUnits.Unit.Equals(_assault))
                    army.assault = guerrillaUnits.Quantity;
                if (guerrillaUnits.Unit.Equals(_engineer))
                    army.engineer = guerrillaUnits.Quantity;
                if (guerrillaUnits.Unit.Equals(_tank))
                    army.tank = guerrillaUnits.Quantity;
            }
            guerrilla.resources = resources;
            guerrilla.buildings = buildings;
            guerrilla.army = army;

            return JsonConvert.SerializeObject(guerrilla);
        }
    }
}
