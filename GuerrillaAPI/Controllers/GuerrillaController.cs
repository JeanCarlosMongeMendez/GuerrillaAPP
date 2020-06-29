using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuerrillaAPI.Model;
using GuerrillaAPI.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace GuerrillaAPI.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
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

        [EnableCors("GetAllPolicy")]
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

                foreach (Guerrilla guerrilla in guerrillas.Where(g => g.Available))
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

        [EnableCors("GetAllPolicy")]
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

        [EnableCors("GetAllPolicy")]
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
                guerrilla.Available = true;
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

        [EnableCors("GetAllPolicy")]
        [HttpPut("{name}/units")]
        public string BuyUnits(string name, units units)
        {
            if (units.buildings == null)
                units.buildings = new buildings();
            if (units.army == null)
                units.army = new army();

            List<Unit> allUnits = _context.Unit.ToList();
            List<GuerrillaResources> guerrillaResources = _context.GuerrillaResources.Where(g => g.Guerrilla.Equals(name)).ToList();

            #region calculate costs
            int oilCost = units.army.assault * allUnits.Where(u => u.Name.Equals(_assault)).Single().Oil +
                    units.army.engineer * allUnits.Where(u => u.Name.Equals(_engineer)).Single().Oil +
                    units.army.tank * allUnits.Where(u => u.Name.Equals(_tank)).Single().Oil +
                    units.buildings.bunker * allUnits.Where(u => u.Name.Equals(_bunker)).Single().Oil;

            int moneyCost = units.army.assault * allUnits.Where(u => u.Name.Equals(_assault)).Single().Money +
                units.army.engineer * allUnits.Where(u => u.Name.Equals(_engineer)).Single().Money +
                units.army.tank * allUnits.Where(u => u.Name.Equals(_tank)).Single().Money +
                units.buildings.bunker * allUnits.Where(u => u.Name.Equals(_bunker)).Single().Money;

            int peopleCost = units.army.assault * allUnits.Where(u => u.Name.Equals(_assault)).Single().People +
                units.army.engineer * allUnits.Where(u => u.Name.Equals(_engineer)).Single().People +
                units.army.tank * allUnits.Where(u => u.Name.Equals(_tank)).Single().People +
                units.buildings.bunker * allUnits.Where(u => u.Name.Equals(_bunker)).Single().People;
            #endregion

            GuerrillaResources guerrillaOil = guerrillaResources.Where(r => r.Resource.Equals(_oil)).Single();
            GuerrillaResources guerrillaMoney = guerrillaResources.Where(r => r.Resource.Equals(_money)).Single();
            GuerrillaResources guerrillaPeople = guerrillaResources.Where(r => r.Resource.Equals(_people)).Single();
            if ((guerrillaOil.Quantity - oilCost) >= 0 && (guerrillaMoney.Quantity - moneyCost) >= 0 && (guerrillaPeople.Quantity - peopleCost) > 0)
            {
                if (units.army.assault > 0)
                {
                    GuerrillaUnits assaultNewUnits = new GuerrillaUnits();
                    assaultNewUnits.Guerrilla = name;
                    assaultNewUnits.Unit = _assault;
                    assaultNewUnits.Quantity = units.army.assault;
                    _context.GuerrillaUnits.Update(assaultNewUnits);
                }

                if (units.army.engineer > 0)
                {
                    GuerrillaUnits engineerNewUnits = new GuerrillaUnits();
                    engineerNewUnits.Guerrilla = name;
                    engineerNewUnits.Unit = _engineer;
                    engineerNewUnits.Quantity = units.army.engineer;
                    _context.GuerrillaUnits.Update(engineerNewUnits);
                }

                if (units.army.tank > 0)
                {
                    GuerrillaUnits tankNewUnits = new GuerrillaUnits();
                    tankNewUnits.Guerrilla = name;
                    tankNewUnits.Unit = _tank;
                    tankNewUnits.Quantity = units.army.tank;
                    _context.GuerrillaUnits.Update(tankNewUnits);
                }

                if (units.buildings.bunker > 0)
                {
                    GuerrillaUnits bunkerNewUnits = new GuerrillaUnits();
                    bunkerNewUnits.Guerrilla = name;
                    bunkerNewUnits.Unit = _bunker;
                    bunkerNewUnits.Quantity = units.buildings.bunker;
                    _context.GuerrillaUnits.Add(bunkerNewUnits);
                }

                guerrillaOil.Quantity = guerrillaOil.Quantity - oilCost;
                guerrillaMoney.Quantity = guerrillaMoney.Quantity - moneyCost;
                guerrillaPeople.Quantity = guerrillaPeople.Quantity - peopleCost;
                _context.GuerrillaResources.Update(guerrillaOil);
                _context.SaveChanges();
            }
            return GetGuerrillaDTO(name);
        }

        [EnableCors("GetAllPolicy")]
        [HttpPost("attack/{guerrillaName}")]
        public string attack(string guerrillaName, string guerrillaSrc)
        {
            Guerrilla guerrillaS = _context.Guerrilla.Where(g => g.Name.Equals(guerrillaSrc)).Single();
            Guerrilla guerrillaT = _context.Guerrilla.Where(g => g.Name.Equals(guerrillaName)).Single();
            if (guerrillaS.Available && guerrillaT.Available)
            {
                guerrillaS.Available = false;
                guerrillaT.Available = false;
                _context.Update(guerrillaS);
                _context.Update(guerrillaT);
                _context.SaveChanges();
                List<GuerrillaResources> guerrillaSrcResources = _context.GuerrillaResources.Where(g => g.Guerrilla.Equals(guerrillaSrc)).ToList();
                List<GuerrillaResources> guerrillaTgtResources = _context.GuerrillaResources.Where(g => g.Guerrilla.Equals(guerrillaName)).ToList();
                List<GuerrillaUnits> guerrillaSrcUnits = _context.GuerrillaUnits.Where(g => g.Guerrilla.Equals(guerrillaSrc)).ToList();
                List<GuerrillaUnits> guerrillaTgtUnits = _context.GuerrillaUnits.Where(g => g.Guerrilla.Equals(guerrillaName)).ToList();
                List<Unit> units = _context.Unit.ToList();
                Team teamSrc = new Team();
                Team teamTgt = new Team();

                teamSrc.GUERRILLA = guerrillaSrc;
                teamSrc.LOSSES = new losses();
                teamSrc.LOOT = new loot();
                teamTgt.GUERRILLA = guerrillaName;
                teamTgt.LOSSES = new losses();

                teamSrc.ASSAULT = guerrillaSrcUnits.Where(g => g.Unit.Equals(_assault)).Single().Quantity;
                teamSrc.ENGINEER = guerrillaSrcUnits.Where(g => g.Unit.Equals(_engineer)).Single().Quantity;
                teamSrc.TANK = guerrillaSrcUnits.Where(g => g.Unit.Equals(_tank)).Single().Quantity;
                teamSrc.BUNKER = guerrillaSrcUnits.Where(g => g.Unit.Equals(_bunker)).Single().Quantity;

                teamSrc.OFFENSE = teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Offense)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Offense)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Offense);

                teamSrc.DEFENSE = teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Defense)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Defense)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Defense);

                teamSrc.LOOT_CAP = teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Loot)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Loot)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Loot);

                //===========================================================================

                teamTgt.ASSAULT = guerrillaTgtUnits.Where(g => g.Unit.Equals(_assault)).Single().Quantity;
                teamTgt.ENGINEER = guerrillaTgtUnits.Where(g => g.Unit.Equals(_engineer)).Single().Quantity;
                teamTgt.TANK = guerrillaTgtUnits.Where(g => g.Unit.Equals(_tank)).Single().Quantity;
                teamTgt.BUNKER = guerrillaTgtUnits.Where(g => g.Unit.Equals(_bunker)).Single().Quantity;

                teamTgt.OFFENSE = teamTgt.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Offense)
                    + teamTgt.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Offense)
                    + teamTgt.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Offense);

                teamTgt.DEFENSE = teamTgt.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Defense)
                    + teamTgt.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Defense)
                    + teamTgt.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Defense)
                    + teamTgt.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Defense);

                teamTgt.DI = ((float)teamTgt.DEFENSE / ((float)teamTgt.DEFENSE + (float)teamSrc.OFFENSE)) + (0.1f);
                teamSrc.AI = ((float)teamSrc.OFFENSE / ((float)teamTgt.DEFENSE + (float)teamSrc.OFFENSE)) + (0.1f);

                //==============================================================================
                teamSrc.LOSSES.ASSAULT = (int)Math.Floor((teamTgt.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Assault)
                    + teamTgt.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Assault)
                    + teamTgt.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Assault)
                    + teamTgt.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Assault)) * teamTgt.DI);

                teamSrc.LOSSES.ENGINEER = (int)Math.Floor((teamTgt.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Engineer)
                    + teamTgt.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Engineer)
                    + teamTgt.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Engineer)
                    + teamTgt.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Engineer)) * teamTgt.DI);

                teamSrc.LOSSES.TANK = (int)Math.Floor((teamTgt.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Tank)
                    + teamTgt.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Tank)
                    + teamTgt.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Tank)
                    + teamTgt.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Tank)) * teamTgt.DI);

                //====================================================================

                teamTgt.LOSSES.ASSAULT = (int)Math.Floor((teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Assault)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Assault)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Assault)
                    + teamSrc.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Assault)) * teamSrc.AI);

                teamTgt.LOSSES.ENGINEER = (int)Math.Floor((teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Engineer)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Engineer)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Engineer)
                    + teamSrc.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Engineer)) * teamSrc.AI);

                teamTgt.LOSSES.TANK = (int)Math.Floor((teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Tank)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Tank)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Tank)
                    + teamSrc.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Tank)) * teamSrc.AI);

                teamTgt.LOSSES.BUNKER = (int)Math.Floor((teamSrc.ASSAULT * (units.Where(u => u.Name.Equals(_assault)).Single().Bunker)
                    + teamSrc.ENGINEER * (units.Where(u => u.Name.Equals(_engineer)).Single().Bunker)
                    + teamSrc.TANK * (units.Where(u => u.Name.Equals(_tank)).Single().Bunker)
                    + teamSrc.BUNKER * (units.Where(u => u.Name.Equals(_bunker)).Single().Bunker)) * teamSrc.AI);

                var rand = new Random();
                teamSrc.LOOT.MONEY = (int)Math.Round(teamSrc.LOOT_CAP * rand.NextDouble());
                teamSrc.LOOT.OIL = (int)Math.Round(teamSrc.LOOT_CAP - teamSrc.LOOT.MONEY);

                guerrillaTgtResources.Where(r => r.Resource.Equals(_oil)).Single().Quantity -= teamSrc.LOOT.OIL;
                guerrillaTgtResources.Where(r => r.Resource.Equals(_money)).Single().Quantity -= teamSrc.LOOT.MONEY;
                guerrillaSrcResources.Where(r => r.Resource.Equals(_oil)).Single().Quantity += teamSrc.LOOT.OIL;
                guerrillaSrcResources.Where(r => r.Resource.Equals(_money)).Single().Quantity += teamSrc.LOOT.MONEY;

                if (guerrillaTgtResources.Where(r => r.Resource.Equals(_oil)).Single().Quantity < 0)
                    guerrillaTgtResources.Where(r => r.Resource.Equals(_oil)).Single().Quantity = 0;
                if (guerrillaTgtResources.Where(r => r.Resource.Equals(_money)).Single().Quantity < 0)
                    guerrillaTgtResources.Where(r => r.Resource.Equals(_money)).Single().Quantity = 0;

                _context.GuerrillaResources.Update(guerrillaTgtResources.Where(r => r.Resource.Equals(_oil)).Single());
                _context.GuerrillaResources.Update(guerrillaTgtResources.Where(r => r.Resource.Equals(_money)).Single());
                _context.GuerrillaResources.Update(guerrillaSrcResources.Where(r => r.Resource.Equals(_oil)).Single());
                _context.GuerrillaResources.Update(guerrillaSrcResources.Where(r => r.Resource.Equals(_money)).Single());

                guerrillaSrcUnits.Where(u => u.Unit.Equals(_assault)).Single().Quantity -= teamSrc.LOSSES.ASSAULT;
                guerrillaSrcUnits.Where(u => u.Unit.Equals(_engineer)).Single().Quantity -= teamSrc.LOSSES.ENGINEER;
                guerrillaSrcUnits.Where(u => u.Unit.Equals(_tank)).Single().Quantity -= teamSrc.LOSSES.TANK;
                guerrillaTgtUnits.Where(u => u.Unit.Equals(_assault)).Single().Quantity -= teamTgt.LOSSES.ASSAULT;
                guerrillaTgtUnits.Where(u => u.Unit.Equals(_engineer)).Single().Quantity -= teamTgt.LOSSES.ENGINEER;
                guerrillaTgtUnits.Where(u => u.Unit.Equals(_tank)).Single().Quantity -= teamTgt.LOSSES.TANK;
                guerrillaTgtUnits.Where(u => u.Unit.Equals(_bunker)).Single().Quantity -= teamTgt.LOSSES.BUNKER;

                if (guerrillaTgtUnits.Where(u => u.Unit.Equals(_assault)).Single().Quantity < 0)
                    guerrillaTgtUnits.Where(u => u.Unit.Equals(_assault)).Single().Quantity = 0;
                if (guerrillaTgtUnits.Where(u => u.Unit.Equals(_engineer)).Single().Quantity < 0)
                    guerrillaTgtUnits.Where(u => u.Unit.Equals(_engineer)).Single().Quantity = 0;
                if (guerrillaTgtUnits.Where(u => u.Unit.Equals(_tank)).Single().Quantity < 0)
                    guerrillaTgtUnits.Where(u => u.Unit.Equals(_tank)).Single().Quantity = 0;
                if (guerrillaTgtUnits.Where(u => u.Unit.Equals(_bunker)).Single().Quantity < 0)
                    guerrillaTgtUnits.Where(u => u.Unit.Equals(_bunker)).Single().Quantity = 0;

                _context.GuerrillaUnits.Update(guerrillaSrcUnits.Where(u => u.Unit.Equals(_assault)).Single());
                _context.GuerrillaUnits.Update(guerrillaSrcUnits.Where(u => u.Unit.Equals(_engineer)).Single());
                _context.GuerrillaUnits.Update(guerrillaSrcUnits.Where(u => u.Unit.Equals(_tank)).Single());
                _context.GuerrillaUnits.Update(guerrillaTgtUnits.Where(u => u.Unit.Equals(_assault)).Single());
                _context.GuerrillaUnits.Update(guerrillaTgtUnits.Where(u => u.Unit.Equals(_engineer)).Single());
                _context.GuerrillaUnits.Update(guerrillaTgtUnits.Where(u => u.Unit.Equals(_tank)).Single());
                _context.GuerrillaUnits.Update(guerrillaTgtUnits.Where(u => u.Unit.Equals(_bunker)).Single());

                _context.SaveChanges();

                guerrillaS.Available = true;
                guerrillaT.Available = true;

                _context.Update(guerrillaS);
                _context.Update(guerrillaT);
                _context.SaveChanges();

                Team[] teams = { teamSrc, teamTgt };
                return JsonConvert.SerializeObject(teams);
            }
            else
            {
                return "ERROR";
            }
        }

        private string GetGuerrillaDTO(string name)
        {
            //load guerrilla
            var guerrilla = (from guerrillaDB in _context.Guerrilla
                             where guerrillaDB.Name.Equals(name)
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
            foreach (GuerrillaResources guerrillaResources in resourcesDB)
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
