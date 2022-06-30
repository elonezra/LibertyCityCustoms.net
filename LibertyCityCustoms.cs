// Decompiled with JetBrains decompiler
// Type: LibertyCityCustoms.Scripts.LibertyCityCustoms
// Assembly: LibertyCityCustoms.net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3EE84B4-074E-4ABA-99F6-4F1E1EEBF973
// Assembly location: D:\Games\Grand Theft Auto IV - The Complete Edition\scripts\LibertyCityCustoms.net.dll

using GTA;
using GTA.@base;
using GTA.Native;
using GTA.value;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LibertyCityCustoms.Scripts
{
  internal class LibertyCityCustoms : Script
  {
    private Vector3 out1 = new Vector3(858.73f, -122.11f, 5.4f);
    private Vector3 in1 = new Vector3(877.74f, -115.06f, 5.52f);
    private Vector3 out2 = new Vector3(737.59f, 1381.7f, 13.69f);
    private Vector3 in2 = new Vector3(724.17f, 1381.92f, 13.86f);
    private Vector3 out3 = new Vector3(-967.1f, 1880.72f, 21.87f);
    private Vector3 in3 = new Vector3(-968.58f, 1895.34f, 21.89f);
    private Vector3 out4 = new Vector3(168.33f, 164.44f, 14.3f);
    private Vector3 in4 = new Vector3(877.74f, -115.06f, 5.52f);
    private Vector3 out5 = new Vector3(-475.43f, 1746.02f, 8.19f);
    private Vector3 in5 = new Vector3(877.74f, -115.06f, 5.52f);
    private Vector3 out6 = new Vector3(-1099.43f, 660.4f, 8.07f);
    private Vector3 in6 = new Vector3(877.74f, -115.06f, 5.52f);
    private Vector3 out7 = new Vector3(1185.16f, 1418.91f, 16.77f);
    private Vector3 in7 = new Vector3(1185.97f, 1393f, 16.99f);
    private Vector3 out8 = new Vector3(618.01f, 1466.86f, 11.35f);
    private Vector3 in8 = new Vector3(618.12f, 1477.94f, 12.42f);
    private Vector3 exit_pos = new Vector3();
    private bool Inside;
    private bool SaveMenu;
    private List<string> menu = new List<string>();
    private List<Vector3> vec = new List<Vector3>();
    private List<string> ten_items = new List<string>();
    private List<Vehicle> spawned_veh = new List<Vehicle>();
    private List<Vehicle> veh_brakes_fast = new List<Vehicle>();
    private List<Vehicle> veh_brakes_fastest = new List<Vehicle>();
    private Texture arrow;
    private Texture bought;
    private string menu_name = "";
    private int i_num;
    private string cur_i = "";
    private int repair_cost;
    private string category_text = "";
    private bool menu_count;
    private string previous_menu = "";
    private bool show_arrows;
    private string i_info = "";
    private float menu_pos_x;
    private float menu_pos_y;
    private int show_from;
    private int show_to = 11;
    private List<ColorIndex> cols = new List<ColorIndex>();
    private ColorIndex base_col;
    private ColorIndex specular_col;
    private ColorIndex extra_col;
    private ColorIndex extra2_col;
    private ColorIndex chosen_col;
    private Light garage_light;
    private Color garage_light_current_color;
    private float light_y;
    private float light_x;
    private float light_z;
    private Vector3 garage_light_position;
    private string garage_light_name;
    private bool Lighting;
    private float intensity;
    private float light_range;
    private Dictionary<Vehicle, Light> lst;
    private Vehicle antiTeftCarSave = null;
    private System.Type colors = typeof (Color);
    private PropertyInfo[] colorInfo;
    private Color cur_light;
    private Color chosen_light;
    private List<VehicleExtra> extr = new List<VehicleExtra>();
    private GTA.Font f;
    private GTA.Font f2 = new GTA.Font("AR DESTINE", 0.9f, (FontScaling) 0, true, false);
    Blip current_saved_car_blip = null;
    // prices
    int repair_price = 0;
    int color_price = 500;
    int wash_price = 20;
    int ATS_Basic_price = 1452;
    int specular_color_price = 700;
    int extra_color_price = 200;
    int extra2_color_price = 170;
    int basic_defense_price = 20000;
    int advance_defense_price = 50000;

        public LibertyCityCustoms()
    {
      this.colorInfo = this.colors.GetProperties(BindingFlags.Static | BindingFlags.Public);
      this.lst = new Dictionary<Vehicle, Light>();
      this.f = new GTA.Font(0.4f, (FontScaling) 0, true, false);
      this.arrow = new Texture(File.ReadAllBytes("./Scripts/LibertyCityCustoms/arrows.png"));
      this.bought = new Texture(File.ReadAllBytes("./Scripts/LibertyCityCustoms/bought.png"));
      if (!File.Exists(this.Settings.Filename))
      {
        this.Settings.Load();
        this.Settings.SetValue("Go_in_garage", "Menu", Keys.Y);
        this.Settings.SetValue("Up", "Menu", Keys.Left);
        this.Settings.SetValue("Down", "Menu", Keys.Right);
        this.Settings.SetValue("x", "Menu", 2f);
        this.Settings.SetValue("y", "Menu", 3.5f);
        this.Settings.SetValue("Do_function", "Menu", Keys.Return);
        this.Settings.SetValue("Back", "Menu", Keys.Back);
        this.Settings.SetValue("enabled", "Garage lighting", false);
        this.Settings.SetValue("range", "Garage lighting", 10f);
        this.Settings.SetValue(nameof (intensity), "Garage lighting", 8f);
        this.Settings.SetValue("x", "Garage lighting", 0.0f);
        this.Settings.SetValue("y", "Garage lighting", 0.0f);
        this.Settings.SetValue("z", "Garage lighting", 4f);
        this.Settings.SetValue("color", "Garage lighting", "Green");
        this.Settings.Save();
      }
      this.garage_light = new Light();
      this.garage_light_position = new Vector3();
      // ISSUE: method pointer
      this.BindKey(this.Settings.GetValueKey("Up", "Menu", Keys.W), new KeyPressDelegate(Move_up));
      // ISSUE: method pointer
      this.BindKey(this.Settings.GetValueKey("Down", "Menu", Keys.S), new KeyPressDelegate(Move_down));
      // ISSUE: method pointer
      this.PerFrameDrawing += new GraphicsEventHandler(menu_draw);
      // ISSUE: method pointer
      this.PerFrameDrawing += new GraphicsEventHandler(goin_draw);
        // ISSUE: method pointer
      this.PerFrameDrawing += new GraphicsEventHandler(money_state);

      this.KeyDown += new GTA.KeyEventHandler(menu_nav);
      this.Tick += new EventHandler(this.ticks);
      this.vec.Add(this.out1);
      this.vec.Add(this.out2);
      this.vec.Add(this.out3);
      this.vec.Add(this.out4);
      this.vec.Add(this.out5);
      this.vec.Add(this.out6);
      this.vec.Add(this.out7);
      this.vec.Add(this.out8);
      foreach (Vector3 vector3 in this.vec)
      {
        Blip blip = Blip.AddBlipContact(vector3);
        blip.Display = (BlipDisplay) 2;
        blip.Icon = (BlipIcon) 75;
        blip.Name = "Liberty City Customs";
      }
      this.menu_pos_x = this.Settings.GetValueFloat("x", "Menu", 2f);
      this.menu_pos_y = this.Settings.GetValueFloat("y", "Menu", 3.5f);
              
      this.light_x = this.Settings.GetValueFloat("x", "Garage lighting", 0.0f);
      this.light_y = this.Settings.GetValueFloat("y", "Garage lighting", 0.0f);
      this.light_z = this.Settings.GetValueFloat("z", "Garage lighting", 4f);
      this.garage_light_name = this.Settings.GetValueString("color", "Garage lighting", "Green");
      this.intensity = this.Settings.GetValueFloat(nameof (intensity), "Garage lighting", 8f);
      this.light_range = this.Settings.GetValueFloat("range", "Garage lighting", 10f);
      this.Lighting = this.Settings.GetValueBool("enabled", "Garage lighting", false);
        Game.Console.Print("LibertyCityCustoms V2.2 by Elon loaded !");
        this.BindPhoneNumber("000", new PhoneDialDelegate(()=>{ Game.DisplayText("you made a call"); }));
            
     

        }
      
    private void ticks(object sender, EventArgs e)
    {

            if (antiTeftCarSave != null)
            {
              

                if (!this.Player.Character.isInVehicle())
                {
                                      
                        if (antiTeftCarSave.HasMetadata("ATS"))
                        {
                            if (current_saved_car_blip == null || !current_saved_car_blip.Exists())
                            {
                                Game.Console.Print("blip not exist");
                                //current_saved_car_blip.Delete();
                                current_saved_car_blip = antiTeftCarSave.AttachBlip();
                               current_saved_car_blip.Icon = BlipIcon.Building_Garage;
                               current_saved_car_blip.Name = "Saved car";
                                current_saved_car_blip.Color = BlipColor.Orange;
                            }
                        }
                }
            
            else
                {
                    //Game.Console.Print("in car");
                     
                    if ((current_saved_car_blip != null && current_saved_car_blip.Exists()))
                    {
                        Game.Console.Print("blip is not exist");
                        current_saved_car_blip.Delete();

                    }
                }
                if (antiTeftCarSave.EngineHealth <= 0)
                {
                    current_saved_car_blip.Delete();
                    antiTeftCarSave.isRequiredForMission = false;
                    antiTeftCarSave = null;
                }

            }            
                           
            
 
            if (this.Player.Character.isInVehicle())
            {
                    if(this.Player.Character.CurrentVehicle.HasMetadata("Defense_level"))
                    {
                        Player.Character.MakeProofTo(true, false, false, false, false);
                    }          
            }
            else
            {
                
                Player.Character.MakeProofTo(false, false, false, false, false);
            }




            if (this.lst.Count > 0)
      {
        foreach (KeyValuePair<Vehicle, Light> keyValuePair in this.lst)
        {
          if (((GTA.@base.Object) keyValuePair.Key).Exists())
          {
            if (this.Exists((object) keyValuePair.Value))
            {
              keyValuePair.Value.Position = keyValuePair.Key.GetOffsetPosition(new Vector3(0.0f, 0.0f, -0.35f));
              keyValuePair.Value.Intensity = 100f;
              keyValuePair.Value.Range = 2.4f;
            }
          }
          else
            keyValuePair.Value.Enabled = false;
        }
      }
      if (this.Player.Character.isInVehicle())
      {
        foreach (Vehicle vehicle in this.veh_brakes_fast)
        {
          if (((GTA.@base.Object) vehicle).Exists())
          {
            if (HandleObject.Equals((HandleObject) this.Player.Character.CurrentVehicle, (HandleObject) vehicle) && (double) vehicle.Speed > 10.0 && this.Player.Character.CurrentVehicle.isOnAllWheels && Game.isKeyPressed(Keys.Space))
              vehicle.Speed -= 0.5f;
          }
          else
            this.veh_brakes_fast.Remove(vehicle);
        }
        foreach (Vehicle vehicle in this.veh_brakes_fastest)
        {
          if (((GTA.@base.Object) vehicle).Exists())
          {
            if (HandleObject.Equals((HandleObject) this.Player.Character.CurrentVehicle, (HandleObject) vehicle) && (double) vehicle.Speed > 10.0 && this.Player.Character.CurrentVehicle.isOnAllWheels && Game.isKeyPressed(Keys.Space))
              vehicle.Speed -= 3f;
          }
          else
            this.veh_brakes_fast.Remove(vehicle);
        }
      }
      if (!this.Inside)
        return;
      /*
      if (this.Lighting)
      {
        this.garage_light_position = this.Player.Character.CurrentVehicle.GetOffsetPosition(new Vector3(this.light_x, this.light_y, this.light_z));
        this.garage_light.Position = this.garage_light_position;
        this.garage_light.Intensity = this.intensity;
        this.garage_light.Range = this.light_range;
        this.garage_light.Color = Color.FromName(this.garage_light_name);
        this.Settings.SetValue("x", "Garage lighting", this.light_x);
        this.Settings.SetValue("y", "Garage lighting", this.light_y);
        this.Settings.SetValue("z", "Garage lighting", this.light_z);
        this.Settings.SetValue("color", "Garage lighting", this.garage_light_name);
        this.Settings.SetValue("intensity", "Garage lighting", this.intensity);
        this.Settings.SetValue("range", "Garage lighting", this.light_range);
      }
      if (this.menu_name == "light position")
      {
        if (Game.isKeyPressed(Keys.A))
          this.light_x -= 0.5f;
        if (Game.isKeyPressed(Keys.D))
          this.light_x += 0.5f;
        if (Game.isKeyPressed(Keys.W))
          this.light_y -= 0.5f;
        if (Game.isKeyPressed(Keys.S))
          this.light_y += 0.5f;
        if (Game.isKeyPressed(Keys.Z))
          this.light_z -= 0.5f;
        if (!Game.isKeyPressed(Keys.X))
          return;
        this.light_z += 0.5f;
      }
      else if (this.menu_name == "light intensity")
      {
        if (Game.isKeyPressed(Keys.A))
          --this.intensity;
        if (!Game.isKeyPressed(Keys.D))
          return;
        ++this.intensity;
      }
      else if (this.menu_name == "light range")
      {
        if (Game.isKeyPressed(Keys.A))
          this.light_range -= 2f;
        if (!Game.isKeyPressed(Keys.D))
          return;
        this.light_range += 2f;
      }
      */
      else
      {
        if (!(this.menu_name == "menu_pos"))
          return;
        this.Settings.SetValue("x", "Menu", this.menu_pos_x);
        this.Settings.SetValue("y", "Menu", this.menu_pos_y);
        if ((double) this.menu_pos_x >= 0.0 && (double) this.menu_pos_x <= 14.8000001907349)
        {
          if (Game.isKeyPressed(Keys.D))
            this.menu_pos_x += 0.1f;
          if (Game.isKeyPressed(Keys.A))
            this.menu_pos_x -= 0.1f;
        }
        else if ((double) this.menu_pos_x < 0.0)
          this.menu_pos_x += 0.1f;
        else if ((double) this.menu_pos_x > 14.8000001907349)
          this.menu_pos_x -= 0.1f;
        if ((double) this.menu_pos_y >= 0.0 && (double) this.menu_pos_y <= 16.0)
        {
          if (Game.isKeyPressed(Keys.W))
            this.menu_pos_y -= 0.1f;
          if (!Game.isKeyPressed(Keys.S))
            return;
          this.menu_pos_y += 0.1f;
        }
        else if ((double) this.menu_pos_y < 0.0)
        {
          this.menu_pos_y += 0.1f;
        }
        else
        {
          if ((double) this.menu_pos_y <= 16.0)
            return;
          this.menu_pos_y -= 0.1f;
        }
      }
    }

    private void Open_menu()
    {
      repair_price = 1030 - this.Player.Character.CurrentVehicle.Health;
      if (this.menu.Count > 0)
        this.menu.Clear();
      if (this.cols.Count > 0)
        this.cols.Clear();
      this.SetUpMenu("", false, "", false);
      if (this.menu_name == "options")
      {
        this.SetUpMenu("CATEGORIES", true, "main", false);
        this.menu.Add("Menu position");
        this.menu.Add("Garage lighting");
      }
      else if (this.menu_name == "menu_pos")
        this.SetUpMenu("MOVE MENU", false, "options", false);
      else if (this.menu_name == "main")
      {
        this.SetUpMenu("CATEGORIES", true, "", false);
        this.menu.Add("Respray");
        this.menu.Add("Brakes");
        this.menu.Add("Defense");
        this.menu.Add("Neons");
        this.menu.Add("Anti Theft system");
        this.menu.Add("Extras");
        this.menu.Add("Wash");
        //this.menu.Add("Options");
        this.menu.Add("Exit");
      }
      else if (this.menu_name == "repair")
      {
        this.SetUpMenu("CATEGORIES", false, "", false);
        this.menu.Add("Repair     " + repair_price + "$");
        this.menu.Add("Exit");
      }
      else if (this.menu_name == "respray")
      {
        this.SetUpMenu("RESPRAYS", false, "main", false);
        this.menu.Add("Primary color   "+color_price+"$");
        this.menu.Add("Specular color  " + specular_color_price+"$");
        this.menu.Add("Featured color  "+ extra_color_price+"$");
        this.menu.Add("Featured2 color  " + extra2_color_price + "$");
      }

      else if (this.menu_name == "Anti Theft system")
      {
        this.SetUpMenu("Anti Theft system", false, "main", false);
        this.menu.Add("Set Basic ATS  " + ATS_Basic_price + "$");
      }

      else if (this.menu_name == "color" || this.menu_name == "specular_color" || this.menu_name == "extra_color" || this.menu_name == "extra2_color")
      {
        this.SetUpMenu("COLORS", true, "respray", true);
        this.SetColors();
               
      }
     /* else if (this.menu_name == "garage lighting")
      {
        this.SetUpMenu("CATEGORIES", true, "options", false);
        if (this.Lighting)
        {
          this.garage_light.Enabled = true;
          this.menu.Add("Position");
          this.menu.Add("Intensity");
          this.menu.Add("Range");
          this.menu.Add("Color");
          this.menu.Add("Disable");
        }
        else
        {
          this.garage_light.Enabled = false;
          this.menu.Add("Enable");
        }
      }
      else if (this.menu_name == "light position")
        this.SetUpMenu("MOVE LIGHT", false, "garage lighting", false);
      else if (this.menu_name == "light intensity")
        this.SetUpMenu("DEC/INC LIGHT INTENSITY", false, "garage lighting", false);
      else if (this.menu_name == "light range")
        this.SetUpMenu("DEC/INC LIGHT RANGE", false, "garage lighting", false);
      else if (this.menu_name == "light color")
      {
        this.SetUpMenu("CHANGE LIGHT COLOR", false, "garage lighting", false);
        for (int index = 0; index < this.colorInfo.Length; ++index)
          this.menu.Add(this.colorInfo[index].Name);
      }*/
      else if (this.menu_name == "brakes")
      {
        this.SetUpMenu("CHANGE QUALITY OF BRAKES", false, "main", false);
        this.menu.Add("Slow");
        this.menu.Add("Fast");
        this.menu.Add("Fastest");
      }
      else if (this.menu_name == "neon")
      {
        this.SetUpMenu("CATEGORIES", false, "main", false);
        if (this.lst.ContainsKey(this.Player.Character.CurrentVehicle))
        {
          foreach (KeyValuePair<Vehicle, Light> keyValuePair in this.lst)
          {
            if (HandleObject.Equals((HandleObject) keyValuePair.Key, (HandleObject) this.Player.Character.CurrentVehicle))
              this.cur_light = keyValuePair.Value.Color;
          }
          this.menu.Add("Disable");
          this.menu.Add("Color");
        }
        else
          this.menu.Add("Enable");
      }
      else if (this.menu_name == "neon color")
      {
        this.SetUpMenu("BY COLORINDEX", true, "neon", true);
        for (int index = 0; index < this.colorInfo.Length; ++index)
          this.menu.Add(this.colorInfo[index].Name);
      }
      else if (this.menu_name == "extras")
      {
        this.SetUpMenu("EXTRAS", false, "main", false);
        this.menu.Add("1");
        this.menu.Add("2");
        this.menu.Add("3");
        this.menu.Add("4");
        this.menu.Add("5");
        this.menu.Add("6");
        this.menu.Add("7");
        this.menu.Add("8");
        this.menu.Add("9");
      }
      else if (this.menu_name == "Defense")
      {
        this.SetUpMenu("Defense", false, "main", false);
        this.menu.Add("Basic defense    "+basic_defense_price + "$");
        this.menu.Add("Advance defense    " + advance_defense_price + "$");
                
      }
      this.show_from = 0;
      this.i_num = 0;
      this.i_info = "";
      if (this.menu.Count <= 0)
        return;
      this.cur_i = this.menu.ToArray()[0];
    }

    private void SetColors()
    {
      this.addcol(ColorIndex.AgateGreen);
      this.addcol(ColorIndex.AlabasterSolid);
      this.addcol(ColorIndex.AntelopeBeige);
      this.addcol(ColorIndex.AnthraciteGrayPoly);
      this.addcol(ColorIndex.ArcticPearl);
      this.addcol(ColorIndex.ArcticWhite);
      this.addcol(ColorIndex.AscotGray);
      this.addcol(ColorIndex.AstraSilverPoly);
      this.addcol(ColorIndex.BisqueFrostPoly);
      this.addcol(ColorIndex.BistonBrownPoly);
      this.addcol(ColorIndex.Black);
      this.addcol(ColorIndex.BlackPoly);
      this.addcol(ColorIndex.BlazeRed);
      this.addcol(ColorIndex.Blue);
      this.addcol(ColorIndex.BrightBluePoly);
      this.addcol(ColorIndex.BrightBluePoly2);
      this.addcol(ColorIndex.BrightBluePoly3);
      this.addcol(ColorIndex.BrightRed);
      this.addcol(ColorIndex.BrightRed2);
      this.addcol(ColorIndex.BrilliantRedPoly);
      this.addcol(ColorIndex.BrilliantRedPoly2);
      this.addcol(ColorIndex.CabernetRedPoly);
      this.addcol(ColorIndex.CherryRed);
      this.addcol(ColorIndex.ClassicRed);
      this.addcol(ColorIndex.ClearCrystalBlueFrostPoly);
      this.addcol(ColorIndex.ConcordBluePoly);
      this.addcol(ColorIndex.CopperBeige);
      this.addcol(ColorIndex.CrystalBluePoly);
      this.addcol(ColorIndex.CurrantBluePoly);
      this.addcol(ColorIndex.CurrantRedPoly);
      this.addcol(ColorIndex.CurrantRedSolid);
      this.addcol(ColorIndex.DarkBeechwoodPoly);
      this.addcol(ColorIndex.DarkGreenPoly);
      this.addcol(ColorIndex.DarkSablePoly);
      this.addcol(ColorIndex.DarkSapphireBluePoly);
      this.addcol(ColorIndex.DarkTitaniumPoly);
      this.addcol(ColorIndex.DeepJewelGreen);
      this.addcol(ColorIndex.DesertRed);
      this.addcol(ColorIndex.DesertRed2);
      this.addcol(ColorIndex.DesertTaupePoly);
      this.addcol(ColorIndex.DiamondBluePoly);
      this.addcol(ColorIndex.ElectricCurrantRedPoly);
      this.addcol(ColorIndex.Flax);
      this.addcol(ColorIndex.FormulaRed);
      this.addcol(ColorIndex.FrostWhite);
      this.addcol(ColorIndex.GarnetRedPoly);
      this.addcol(ColorIndex.GracefulRedMica);
      this.addcol(ColorIndex.GrayPoly);
      this.addcol(ColorIndex.Green);
      this.addcol(ColorIndex.Green2);
      this.addcol(ColorIndex.GunMetalPoly);
      this.addcol(ColorIndex.HarborBluePoly);
      this.addcol(ColorIndex.HoneyBeigePoly);
      this.addcol(ColorIndex.Hoods);
      this.addcol(ColorIndex.JasperGreenPoly);
      this.addcol(ColorIndex.LammyOrange);
      this.addcol(ColorIndex.LammyYellow);
      this.addcol(ColorIndex.LightBeechwoodPoly);
      this.addcol(ColorIndex.LightBlueGrey);
      this.addcol(ColorIndex.LightChampagnePoly);
      this.addcol(ColorIndex.LightCrystalBluePoly);
      this.addcol(ColorIndex.LightDriftwoodPoly);
      this.addcol(ColorIndex.LightIvory);
      this.addcol(ColorIndex.LightSapphireBluePoly);
      this.addcol(ColorIndex.LightSapphireBluePoly2);
      this.addcol(ColorIndex.LightTitaniumPoly);
      this.addcol(ColorIndex.MalachitePoly);
      this.addcol(ColorIndex.MarinerBlue);
      this.addcol(ColorIndex.MediumBeechwoodPoly);
      this.addcol(ColorIndex.MediumCabernetSolid);
      this.addcol(ColorIndex.MediumFlax);
      this.addcol(ColorIndex.MediumGarnetRedPoly);
      this.addcol(ColorIndex.MediumGrayPoly);
      this.addcol(ColorIndex.MediumGrayPoly2);
      this.addcol(ColorIndex.MediumMauiBluePoly);
      this.addcol(ColorIndex.MediumRedSolid);
      this.addcol(ColorIndex.MediumSandalwoodPoly);
      this.addcol(ColorIndex.MediumSapphireBlueFiremist);
      this.addcol(ColorIndex.MediumSapphireBluePoly);
      this.addcol(ColorIndex.MedRegattaBluePoly);
      this.addcol(ColorIndex.MellowBurgundy);
      this.addcol(ColorIndex.MidnightBlue);
      this.addcol(ColorIndex.NassauBluePoly);
      this.addcol(ColorIndex.NauticalBluePoly);
      this.addcol(ColorIndex.OxfordWhiteSolid);
      this.addcol(ColorIndex.PastelAlabaster);
      this.addcol(ColorIndex.PastelAlabasterSolid);
      this.addcol(ColorIndex.PetrolBlueGreenPoly);
      this.addcol(ColorIndex.PewterGrayPoly);
      this.addcol(ColorIndex.PoliceCarBlue);
      this.addcol(ColorIndex.PoliceWhite);
      this.addcol(ColorIndex.PorcelainSilverPoly);
      this.addcol(ColorIndex.PuebloBeige);
      this.addcol(ColorIndex.RaceYellowSolid);
      this.addcol(ColorIndex.RioRed);
      this.addcol(ColorIndex.SandalwoodFrostPoly);
      this.addcol(ColorIndex.SaxonyBluePoly);
      this.addcol(ColorIndex.SeafoamPoly);
      this.addcol(ColorIndex.SeafoamPoly2);
      this.addcol(ColorIndex.SecuricorDarkGreen);
      this.addcol(ColorIndex.SecuricorDarkGreen2);
      this.addcol(ColorIndex.SecuricorLightGray);
      this.addcol(ColorIndex.ShadowSilverPoly);
      this.addcol(ColorIndex.SilverPoly);
      this.addcol(ColorIndex.SilverStonePoly);
      this.addcol(ColorIndex.SilverStonePoly2);
      this.addcol(ColorIndex.SlateGray);
      this.addcol(ColorIndex.SmokeSilverPoly);
      this.addcol(ColorIndex.SpinnakerBlueSolid);
      this.addcol(ColorIndex.SteelBluePoly);
      this.addcol(ColorIndex.SteelGrayPoly);
      this.addcol(ColorIndex.SteelGrayPoly2);
      this.addcol(ColorIndex.StrikingBlue);
      this.addcol(ColorIndex.SurfBlue);
      this.addcol(ColorIndex.TaxiYellow);
      this.addcol(ColorIndex.TaxiYellow2);
      this.addcol(ColorIndex.TempleCurtainPurple);
      this.addcol(ColorIndex.TitaniumFrostPoly);
      this.addcol(ColorIndex.TorchRed);
      this.addcol(ColorIndex.TorinoRedPearl);
      this.addcol(ColorIndex.TurismoRed);
      this.addcol(ColorIndex.TwilightBluePoly);
      this.addcol(ColorIndex.TwilightBluePoly2);
      this.addcol(ColorIndex.UltraBluePoly);
      this.addcol(ColorIndex.VermilionSolid);
      this.addcol(ColorIndex.VermillionSolid);
      this.addcol(ColorIndex.VeryRed);
      this.addcol(ColorIndex.VeryWhite);
      this.addcol(ColorIndex.WarmGreyMica);
      this.addcol(ColorIndex.White);
      this.addcol(ColorIndex.WhiteDiamondPearl);
      this.addcol(ColorIndex.WildStrawberryPoly);
      this.addcol(ColorIndex.WinningSilverPoly);
      this.addcol(ColorIndex.WoodrosePoly);
     /* if (Game.CurrentEpisode == 2)
      {
        this.addcol(ColorIndex.op_Implicit(134));
        this.addcol(ColorIndex.op_Implicit(135));
        this.addcol(ColorIndex.op_Implicit(136));
      }*/
      foreach (ColorIndex col in this.cols)
        this.menu.Add(((ColorIndex) col).Name);
    }

    private void SetUpMenu(string category, bool m_count, string prev_menu, bool show_arrow)
    {
      this.category_text = category;
      this.menu_count = m_count;
      this.previous_menu = prev_menu;
      this.show_arrows = show_arrow;
    }

    private void addcol(ColorIndex c) => this.cols.Add(c);

    private void Move_up()
    {
      if (this.menu.Count <= 0 || this.i_num <= 0)
        return;
      --this.i_num;
      this.cur_i = this.menu[this.i_num];
      if (this.menu.Count <= 11 || this.i_num >= this.show_from || this.show_from <= 0)
        return;
      this.ten_items.Clear();
      --this.show_from;
      this.ten_items = this.menu.GetRange(this.show_from, this.show_to);
    }

    private void Move_down()
    {
      if (this.menu.Count <= 0 || this.i_num >= this.menu.Count - 1)
        return;
      ++this.i_num;
      this.cur_i = this.menu[this.i_num];
      if (this.menu.Count <= 11 || this.i_num <= this.show_from + 10 || this.show_from >= this.menu.Count)
        return;
      this.ten_items.Clear();
      ++this.show_from;
      this.ten_items = this.menu.GetRange(this.show_from, this.show_to);
    }

    private void SetInGarage(Vector3 position, float heading, Vector3 ex_pos)
    {
        Function.Call("FORCE_LOADING_SCREEN", new Parameter[1] { 1 });
        Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
     
      this.Player.Character.CurrentVehicle.Position = ((Vector3) position).ToGround();
            this.Wait(3000);
            Function.Call("FORCE_LOADING_SCREEN", new Parameter[1] { 0 });
            currentVehicle.Heading = heading;
      this.exit_pos = ex_pos;
      currentVehicle.PlaceOnGroundProperly();
      currentVehicle.FreezePosition = true;
      currentVehicle.DoorLock = (DoorLock) 4;
      currentVehicle.InteriorLightOn = false;
      currentVehicle.HazardLightsOn = false;
      this.Inside = true;
      this.garage_light.Enabled = true;
      this.garage_light.Range = this.intensity;
      


      //make the vehicle repair
      if (this.Player.Character.CurrentVehicle.Health < 1000)
      {
        this.repair_cost = 1000 - this.repair_cost;
        this.menu_name = "repair";
        this.Open_menu();
      }
      else
      {
        this.menu_name = "main";
        this.Open_menu();
      }
    }

    private void SetOutOfGarage()
    {
      Function.Call("FORCE_LOADING_SCREEN", new Parameter[1] { 1 });
      this.Inside = false;
      this.Player.Character.CurrentVehicle.Position = (this.exit_pos).ToGround();
      this.Player.Character.CurrentVehicle.PlaceOnGroundProperly();
      this.Player.Character.CurrentVehicle.FreezePosition = false;
      this.Player.Character.CurrentVehicle.DoorLock = (DoorLock) 0;
      this.i_info = "";
      this.garage_light.Enabled = false;
      this.Wait(2000);
     Function.Call("FORCE_LOADING_SCREEN", new Parameter[1] { 0 });

        }

        private void GoBack()
    {
      if (this.previous_menu == "")
      {
        if (this.SaveMenu)
          this.SaveMenu = false;
        else
          this.SetOutOfGarage();
      }
      else
      {
        this.menu_name = this.previous_menu;
        this.Open_menu();
      }
    }

    private void NextMenu(string next_menu)
    {
      this.menu_name = next_menu;
      this.Open_menu();
    }

    private bool paying(int amount)
        {
               

                if (this.Player.Money - amount < 0)
                {
                    Game.DisplayText("not enough money, lack of " + amount + "$");
                    return false;
                }
              
                this.Player.Money = this.Player.Money - amount;
            return true;
            }
    private void menu_nav(object sender, GTA.KeyEventArgs e)
    {
      if (!this.Inside)
      {
        if (this.SaveMenu || e.Key != this.Settings.GetValueKey("Go_in_garage", "Menu", Keys.Y)||!this.Player.Character.isInVehicle())
          return;
        Model model = this.Player.Character.CurrentVehicle.Model;
        if (!((Model) model).isCar)
          return;
        Vector3 position1 = this.Player.Character.Position;
        if ((double) ((Vector3) position1).DistanceTo(this.out1) < 4.0)
        {
          this.SetInGarage(((Vector3) this.in1).ToGround(), 88.77f, this.out1);
        }
        else
        {
          Vector3 position2 = this.Player.Character.Position;
          if ((double) ((Vector3) position2).DistanceTo(this.out2) < 4.0)
          {
            this.SetInGarage(((Vector3) this.in2).ToGround(), 359.8f, this.out2);
          }
          else
          {
            Vector3 position3 = this.Player.Character.Position;
            if ((double) ((Vector3) position3).DistanceTo(this.out3) < 4.0)
            {
              this.SetInGarage(((Vector3) this.in3).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out3);
            }
            else
            {
              Vector3 position4 = this.Player.Character.Position;
              if ((double) ((Vector3) position4).DistanceTo(this.out4) < 4.0)
              {
                this.SetInGarage(((Vector3) this.in4).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out4);
              }
              else
              {
                Vector3 position5 = this.Player.Character.Position;
                if ((double) ((Vector3) position5).DistanceTo(this.out5) < 4.0)
                {
                  this.SetInGarage(((Vector3) this.in5).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out5);
                }
                else
                {
                  Vector3 position6 = this.Player.Character.Position;
                  if ((double) ((Vector3) position6).DistanceTo(this.out6) < 4.0)
                  {
                    this.SetInGarage(((Vector3) this.in6).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out6);
                  }
                  else
                  {
                    Vector3 position7 = this.Player.Character.Position;
                    if ((double) ((Vector3) position7).DistanceTo(this.out7) < 4.0)
                    {
                      this.SetInGarage(((Vector3) this.in7).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out7);
                    }
                    else
                    {
                      Vector3 position8 = this.Player.Character.Position;
                      if ((double) ((Vector3) position8).DistanceTo(this.out8) >= 4.0)
                        return;
                      this.SetInGarage(((Vector3) this.in8).ToGround(), this.Player.Character.CurrentVehicle.Heading, this.out8);
                    }
                  }
                }
              }
            }
          }
        }
      }
      else if (e.Key == this.Settings.GetValueKey("Do_function", "Menu", Keys.Return))
      {
        if (this.menu.Count <= 0)
          return;
                if (this.cur_i == "Back" || this.cur_i == "Exit")
                    this.GoBack();
                else if (this.menu_name == "repair")
                {
                    if (!(this.cur_i == ("Repair     " + repair_price + "$")))
                        return;
                    if(paying(repair_price))
                    {
                        this.Player.Character.CurrentVehicle.Repair();
                    }
                    
                    this.NextMenu("main");
                }
                else if (this.menu_name == "main")
                {
                    if (this.cur_i == "Respray")
                        this.NextMenu("respray");
                    else if (this.cur_i == "Options")
                        this.NextMenu("options");
                    else if (this.cur_i == "Brakes")
                        this.NextMenu("brakes");
                    else if (this.cur_i == "Anti Theft system")
                        this.NextMenu("Anti Theft system");
                    else if (this.cur_i == "Neons")
                        this.NextMenu("neon");
                    else if (this.cur_i == "Wash")
                    {
                        if (paying(wash_price))
                        {
                            Function.Call<int>("START_PTFX_ON_VEH", new Parameter[9]{"shot_directed_water",
                              this.Player.Character.CurrentVehicle,-4.7f,-5f,8f,130f,0.0f,500f,5f });
                            Function.Call<int>("START_PTFX_ON_VEH", new Parameter[9] {"shot_directed_water",
                              this.Player.Character.CurrentVehicle,7f,0.0f,8f,130f,0.0f,3500f,5f});
                                this.Inside = false;
                            this.Wait(2000);
                            this.Player.Character.CurrentVehicle.Wash();
                                 this.Wait(2000);
                                this.Inside = true;
                                
                         

                          
                        }
                    }
                    
                    else if (this.cur_i == "Extras")
                    {
                        this.NextMenu("extras");
                    }
                    else if(this.cur_i == "Defense")
                        this.NextMenu("Defense");
                }
                else if (this.menu_name == "respray")
                {
                    if (this.cur_i.Contains("Primary color"))
                        this.NextMenu("color");
                    else if (this.cur_i.Contains("Specular color"))
                        this.NextMenu("specular_color");
                    else if (this.cur_i.Contains("Featured color"))
                        this.NextMenu("extra_color");
                    else if (this.cur_i.Contains("Featured2 color"))
                        this.NextMenu("extra2_color");
                }
                else if (this.menu_name == "color")
                {
                    if (this.Player.Money - color_price < 0)
                        Game.DisplayText("Not enough money");
                    else
                    {
                        this.Player.Money = this.Player.Money - color_price;
                        this.base_col = this.chosen_col;
                    }

                }
                else if (this.menu_name == "specular_color")
                {
                    
                    if (paying(specular_color_price))
                       this.specular_col = this.chosen_col;
              
                }

                else if (this.menu_name == "extra_color")
                {
                    if (paying(specular_color_price))
                        this.extra_col = this.chosen_col;
                    
                }

                else if (this.menu_name == "extra2_color")
                {
                    
                    if (paying(extra2_color_price))
                        this.extra2_col = this.chosen_col;
                   
                }
                /*
                else if (this.menu_name == "options")
                {
                    if (this.cur_i == "Menu position")
                    {
                        this.NextMenu("menu_pos");
                    }
                    else
                    {
                        if (!(this.cur_i == "Garage lighting"))
                            return;
                        this.NextMenu("garage lighting");
                    }
                }
                */
                else if (this.menu_name == "garage lightin")
                {
                    this.garage_light_current_color = Color.FromName(this.garage_light_name);
                    this.garage_light_name = this.garage_light_current_color.Name;
                    if (this.cur_i == "Position")
                        this.NextMenu("light position");
                    else if (this.cur_i == "Intensity")
                        this.NextMenu("light intensity");
                    else if (this.cur_i == "Color")
                        this.NextMenu("light color");
                    else if (this.cur_i == "Enable" | this.cur_i == "Disable")
                    {
                        this.Lighting = !this.Lighting;
                        this.Settings.SetValue("enabled", "Garage lighting", this.Lighting);
                        this.NextMenu("garage lighting");
                    }
                    else
                    {
                        if (!(this.cur_i == "Range"))
                            return;
                        this.NextMenu("light range");
                    }
                }
                else if (this.menu_name == "light color")
                    this.garage_light_current_color = Color.FromName(this.garage_light_name);
                else if (this.menu_name == "brakes")
                {
                    Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
                    if (this.cur_i == "Slow")
                    {
                        if (this.veh_brakes_fast.Contains(currentVehicle))
                        {
                            this.veh_brakes_fast.Remove(currentVehicle);
                        }
                        else
                        {
                            if (!this.veh_brakes_fastest.Contains(currentVehicle))
                                return;
                            this.veh_brakes_fastest.Remove(currentVehicle);
                        }
                    }
                    else if (this.cur_i == "Fast")
                    {
                        if (this.veh_brakes_fast.Contains(currentVehicle))
                            return;
                        if (this.veh_brakes_fastest.Contains(currentVehicle))
                        {
                            this.veh_brakes_fastest.Remove(currentVehicle);
                            this.veh_brakes_fast.Add(currentVehicle);
                        }
                        else
                            this.veh_brakes_fast.Add(currentVehicle);
                    }
                    else
                    {
                        if (!(this.cur_i == "Fastest"))
                            return;
                        if (this.veh_brakes_fast.Contains(currentVehicle))
                        {
                            this.veh_brakes_fast.Remove(currentVehicle);
                            this.veh_brakes_fastest.Add(currentVehicle);
                        }
                        else
                        {
                            if (this.veh_brakes_fastest.Contains(currentVehicle))
                                return;
                            this.veh_brakes_fastest.Add(currentVehicle);
                        }
                    }
                }
                else if (this.menu_name == "neon")
                {
                    Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
                    Light light = new Light();
                    light.Enabled = true;
                    Dictionary<Vehicle, Light> dictionary = new Dictionary<Vehicle, Light>((IDictionary<Vehicle, Light>)this.lst);
                    if (this.lst.ContainsKey(currentVehicle))
                    {
                        foreach (KeyValuePair<Vehicle, Light> keyValuePair in dictionary)
                        {
                            if (HandleObject.Equals((HandleObject)keyValuePair.Key, (HandleObject)this.Player.Character.CurrentVehicle))
                            {
                                if (this.cur_i == "Disable")
                                {
                                    keyValuePair.Value.Disable();
                                    this.lst.Remove(keyValuePair.Key);
                                    this.NextMenu("neon");
                                }
                                else if (this.cur_i == "Color")
                                    this.NextMenu("neon color");
                            }
                        }
                    }
                    else
                    {
                        if (!(this.cur_i == "Enable"))
                            return;
                        this.lst.Add(currentVehicle, light);
                        this.NextMenu("neon");
                    }
                }
                else if (this.menu_name == "neon color")
                {
                    Dictionary<Vehicle, Light> dictionary = new Dictionary<Vehicle, Light>((IDictionary<Vehicle, Light>)this.lst);
                    if (!this.lst.ContainsKey(this.Player.Character.CurrentVehicle))
                        return;
                    foreach (KeyValuePair<Vehicle, Light> keyValuePair in dictionary)
                    {
                        if (HandleObject.Equals((HandleObject)keyValuePair.Key, (HandleObject)this.Player.Character.CurrentVehicle))
                        {
                            this.cur_light = this.chosen_light;
                            keyValuePair.Value.Color = this.cur_light;
                        }
                    }
                }
                else if (this.menu_name == "extras")
                {
                    Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
                    if (this.cur_i == "1")
                        this.turn_extra(1);
                    else if (this.cur_i == "2")
                        this.turn_extra(2);
                    else if (this.cur_i == "3")
                        this.turn_extra(3);
                    else if (this.cur_i == "4")
                        this.turn_extra(4);
                    else if (this.cur_i == "5")
                        this.turn_extra(5);
                    else if (this.cur_i == "6")
                        this.turn_extra(6);
                    else if (this.cur_i == "7")
                        this.turn_extra(7);
                    else if (this.cur_i == "8")
                    {
                        this.turn_extra(8);
                    }
                    else
                    {
                        if (!(this.cur_i == "9"))
                            return;
                        this.turn_extra(9);
                    }
                }
                else if(this.menu_name == "Anti Theft system")
                {
                    if(this.cur_i.Contains("Set Basic ATS"))
                    {
                        if(paying(ATS_Basic_price))
                        {
                            this.Player.Character.CurrentVehicle.SetMetadata("ATS", false, 1);
                            this.Player.Character.CurrentVehicle.isRequiredForMission = true;
                            this.antiTeftCarSave = (Vehicle)Player.Character.CurrentVehicle;
                        }
                       
                        
                    }
                }
                else if((this.menu_name == "Defense"))
                {

                    if (this.cur_i.StartsWith("Basic defense"))
                    {
                        if(paying(basic_defense_price))
                        {
                            this.Player.Character.CurrentVehicle.SetMetadata("Defense_level", true, 1);
                            
                            this.Player.Character.CurrentVehicle.MakeProofTo(true, false, false, false, false);
                        }
                    }
                    else if(this.cur_i.StartsWith("Advance defense"))
                    {
                        if (paying(advance_defense_price))
                        {
                            this.Player.Character.CurrentVehicle.MakeProofTo(true, true, false, true, true);
                            this.Player.Character.CurrentVehicle.CanBeDamaged = true;
                            this.Player.Character.CurrentVehicle.CanBeVisiblyDamaged = false;
                            this.Player.Character.CurrentVehicle.SetMetadata("Defense_level", true, 2);

                        }
                    }
                    
                }
      }
      else
      {
        if (e.Key != this.Settings.GetValueKey("Back", "Menu", Keys.Back))
          return;
        if (this.menu_name == "color")
        {

                this.chosen_col = this.base_col;
                this.Player.Character.CurrentVehicle.Color = this.chosen_col;
             
                this.GoBack();
         
            
        }
        else if (this.menu_name == "specular_color")
        {

          this.chosen_col = this.specular_col;
          this.Player.Character.CurrentVehicle.SpecularColor = this.chosen_col;
         this.GoBack();
        }
        else if (this.menu_name == "extra_color")
        {

        this.chosen_col = this.extra_col;
        this.Player.Character.CurrentVehicle.FeatureColor1 = this.chosen_col;
        this.GoBack();
                    
        }
        else if (this.menu_name == "extra2_color")
        {
          this.chosen_col = this.extra2_col;
          this.Player.Character.CurrentVehicle.FeatureColor2 = this.chosen_col;
          this.GoBack();
        }
        else if (this.menu_name == "neon color")
        {
          foreach (KeyValuePair<Vehicle, Light> keyValuePair in this.lst)
          {
            if (HandleObject.Equals((HandleObject) keyValuePair.Key, (HandleObject) this.Player.Character.CurrentVehicle) && keyValuePair.Value.Enabled)
            {
              this.chosen_light = this.cur_light;
              keyValuePair.Value.Color = this.chosen_light;
            }
          }
          this.GoBack();
        }
        else if (this.menu_name == "light color")
        {
          this.garage_light_name = this.garage_light_current_color.Name;
          this.GoBack();
        }
        else
          this.GoBack();
      }
    }

    private void goin_draw(object sender, GraphicsEventArgs e)
    {
      e.Graphics.Scaling = (FontScaling) 0;
      if (this.Inside || this.SaveMenu || !this.Player.Character.isInVehicle())
        return;
      Model model = this.Player.Character.CurrentVehicle.Model;
      if (!((Model) model).isCar)
        return;
      foreach (Vector3 vector3 in this.vec)
      {
        Vector3 position = this.Player.Character.Position;
        if ((double) ((Vector3) position).DistanceTo(vector3) < 4.0)
          e.Graphics.DrawText("Press Y to get in to the garage", 0.3f, 0.3f);
      }
    }
    private void money_state(object sender, GraphicsEventArgs e)
    {
            
       e.Graphics.Scaling = (FontScaling)2;
            
        if ((!this.Inside) || this.SaveMenu || !this.Player.Character.isInVehicle())
            return;
        Model model = this.Player.Character.CurrentVehicle.Model;
        if (!((Model)model).isCar)
            return;

        e.Graphics.DrawText(this.Player.Money+"$", 1024f, 100f,f2);
       
    }



    private void turn_extra(int id)
    {
      if (this.Player.Character.CurrentVehicle.Extras(id).Enabled)
        this.Player.Character.CurrentVehicle.Extras(id).Enabled = false;
      else
        this.Player.Character.CurrentVehicle.Extras(id).Enabled = true;
    }

    private void DrawMenuText(
      GraphicsEventArgs e,
      string text,
      float m_x,
      float m_y,
      float txt_x,
      float txt_y,
      Color box_col,
      Color txt_col)
    {
      e.Graphics.DrawRectangle(m_x, m_y, 5.5f, 0.6f, box_col);
      e.Graphics.DrawText(text, txt_x - 0.03f, txt_y - 0.03f, Color.Black, this.f);
      e.Graphics.DrawText(text, txt_x, txt_y, txt_col, this.f);
    }

    private void DrawBought(GraphicsEventArgs e, float x, float y) => e.Graphics.DrawSprite(this.bought, new RectangleF(x + 4.8f, y - 0.59f, 0.45f, 0.6f));

    private void menu_draw(object sender, GraphicsEventArgs e)
    {
      if (!this.Inside)
        return;
      e.Graphics.Scaling = (FontScaling) 0;
      float menuPosY = this.menu_pos_y;
      float menuPosX = this.menu_pos_x;
      e.Graphics.DrawText("Liberty City Customs", new RectangleF(menuPosX, menuPosY - 1f, 5f, 1f), (TextAlignment) 16, Color.White, this.f2);
      float y;
      if (this.menu_name == "respray")
      {
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        y = menuPosY + 0.58f;
        this.base_col = this.Player.Character.CurrentVehicle.Color;
        this.specular_col = this.Player.Character.CurrentVehicle.SpecularColor;
        this.extra_col = this.Player.Character.CurrentVehicle.FeatureColor1;
        this.extra2_col = this.Player.Character.CurrentVehicle.FeatureColor2;
      }
      else if (this.menu_name == "color")
      {
        int knownColor = (int) Color.White.ToKnownColor();
        this.Player.Character.CurrentVehicle.Color = this.chosen_col;
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        e.Graphics.DrawText(this.i_num.ToString() + "/" + (object) (this.menu.Count - 1), menuPosX + 4.7f, menuPosY + 0.1f, Color.White, this.f);
        y = menuPosY + 0.58f;
        if (this.cur_i == "Back")
        {
          this.chosen_col = this.base_col;
        }
        else
        {
          foreach (ColorIndex col in this.cols)
          {
            if (((ColorIndex) col).Name == this.cur_i)
              this.chosen_col = col;
          }
        }
      }
      else if (this.menu_name == "specular_color")
      {
        this.Player.Character.CurrentVehicle.SpecularColor = this.chosen_col;
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        e.Graphics.DrawText(this.i_num.ToString() + "/" + (object) (this.menu.Count - 1), menuPosX + 4.7f, menuPosY + 0.1f, Color.White, this.f);
        y = menuPosY + 0.58f;
        if (this.cur_i == "Back")
        {
          this.chosen_col = this.specular_col;
        }
        else
        {
          foreach (ColorIndex col in this.cols)
          {
            if (((ColorIndex) col).Name == this.cur_i)
              this.chosen_col = col;
          }
        }
      }
      else if (this.menu_name == "extra_color")
      {
        this.Player.Character.CurrentVehicle.FeatureColor1 = this.chosen_col;
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        e.Graphics.DrawText(this.i_num.ToString() + "/" + (object) (this.menu.Count - 1), menuPosX + 4.7f, menuPosY + 0.1f, Color.White, this.f);
        y = menuPosY + 0.58f;
        if (this.cur_i == "Back")
        {
          this.chosen_col = this.extra_col;
        }
        else
        {
          foreach (ColorIndex col in this.cols)
          {
            if (((ColorIndex) col).Name == this.cur_i)
              this.chosen_col = col;
          }
        }
      }
      else if (this.menu_name == "extra2_color")
      {
        this.Player.Character.CurrentVehicle.FeatureColor2 = this.chosen_col;
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        e.Graphics.DrawText(this.i_num.ToString() + "/" + (object) (this.menu.Count - 1), menuPosX + 4.7f, menuPosY + 0.1f, Color.White, this.f);
        y = menuPosY + 0.58f;
        if (this.cur_i == "Back")
        {
          this.chosen_col = this.extra2_col;
        }
        else
        {
          foreach (ColorIndex col in this.cols)
          {
            if (((ColorIndex) col).Name == this.cur_i)
              this.chosen_col = col;
          }
        }
      }
      else
      {
        this.DrawMenuText(e, this.category_text, menuPosX + 2.6f, menuPosY + 0.26f, menuPosX, menuPosY + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        if (this.menu_count)
          e.Graphics.DrawText(this.i_num.ToString() + "/" + (object) (this.menu.Count - 1), menuPosX + 4.7f, menuPosY + 0.1f, Color.White, this.f);
        y = menuPosY + 0.58f;
      }
      if (this.menu.Count > 0 && this.menu.Count >= this.show_to)
      {
        this.ten_items.Clear();
        if (this.ten_items.Count == 0)
          this.ten_items = this.menu.GetRange(this.show_from, this.show_to);
        else
          this.ten_items.Clear();
      }
      if (this.menu_name == "light position")
      {
        this.DrawMenuText(e, "x:= " + (object) this.light_x + " y:= " + (object) this.light_y + " z:= " + (object) this.light_z, (float) ((double) menuPosX + 2.59999990463257), (float) ((double) y + 0.259999990463257), menuPosX, (float) ((double) y + 0.100000001490116), Color.FromArgb(150, 0, 0, 0), Color.White);
        y += 0.58f;
      }
      else if (this.menu_name == "light intensity")
      {
        this.DrawMenuText(e, "intensity:= " + (object) this.intensity, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        y += 0.58f;
      }
      else if (this.menu_name == "light range")
      {
        this.DrawMenuText(e, "range:= " + (object) this.light_range, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        y += 0.58f;
      }
      else if (this.menu_name == "menu_pos")
      {
        this.DrawMenuText(e, "x:= " + (object) this.menu_pos_x + " y:= " + (object) this.menu_pos_y, (float) ((double) menuPosX + 2.59999990463257), (float) ((double) y + 0.259999990463257), menuPosX, (float) ((double) y + 0.100000001490116), Color.FromArgb(150, 0, 0, 0), Color.White);
        y += 0.58f;
      }
      else if (this.menu_name == "light color")
      {
        for (int index = 0; index < this.colorInfo.Length; ++index)
        {
          if (this.cur_i == this.colorInfo[index].Name)
            this.garage_light_name = this.colorInfo[index].Name;
        }
      }
      else if (this.menu_name == "neon color" && !this.menu.Contains("Disable"))
      {
        for (int index = 0; index < this.colorInfo.Length; ++index)
        {
          if (this.cur_i == this.colorInfo[index].Name)
          {
            foreach (KeyValuePair<Vehicle, Light> keyValuePair in this.lst)
            {
              if (HandleObject.Equals((HandleObject) keyValuePair.Key, (HandleObject) this.Player.Character.CurrentVehicle) && keyValuePair.Value.Enabled)
              {
                this.chosen_light = Color.FromName(this.colorInfo[index].Name);
                keyValuePair.Value.Color = this.chosen_light;
              }
            }
          }
        }
      }
      if (this.menu.Count > 11)
      {
        foreach (string tenItem in this.ten_items)
        {
          if (this.cur_i == tenItem)
          {
            this.DrawMenuText(e, tenItem, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(200, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.White);
            y += 0.58f;
          }
          else
          {
            this.DrawMenuText(e, tenItem, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
            y += 0.58f;
          }
          if (this.menu_name == "neon color")
          {
            Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
            for (int index = 0; index < this.colorInfo.Length; ++index)
            {
              if (tenItem == this.colorInfo[index].Name && tenItem == this.cur_light.Name)
                this.DrawBought(e, menuPosX, y);
            }
          }
          else if (this.menu_name == "light color")
          {
            for (int index = 0; index < this.colorInfo.Length; ++index)
            {
              if (tenItem == this.colorInfo[index].Name && tenItem == this.garage_light_current_color.Name)
                this.DrawBought(e, menuPosX, y);
            }
          }
          else if (this.menu_name == "color")
          {
            foreach (ColorIndex col in this.cols)
            {
              if (tenItem == ((ColorIndex) col).Name && ((ColorIndex) this.base_col).Name == tenItem)
                this.DrawBought(e, menuPosX, y);
            }
          }
          else if (this.menu_name == "specular_color")
          {
            foreach (ColorIndex col in this.cols)
            {
              if (tenItem == ((ColorIndex) col).Name && ((ColorIndex)   this.specular_col).Name == tenItem)
                this.DrawBought(e, menuPosX, y);
            }
          }
          else if (this.menu_name == "extra_color")
          {
            foreach (ColorIndex col in this.cols)
            {
              if (tenItem == ((ColorIndex) col).Name && ((ColorIndex)  this.extra_col).Name == tenItem)
                this.DrawBought(e, menuPosX, y);
            }
          }
          else if (this.menu_name == "extra2_color")
          {
            foreach (ColorIndex col in this.cols)
            {
              if (tenItem == ((ColorIndex) col).Name && ((ColorIndex) this.extra2_col).Name == tenItem)
                this.DrawBought(e, menuPosX, y);
            }
          }
        }
      }
      else
      {
        foreach (string text in this.menu)
        {
          if (this.cur_i == text)
          {
            this.DrawMenuText(e, text, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(200, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.White);
            y += 0.58f;
          }
          else
          {
            this.DrawMenuText(e, text, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
            y += 0.58f;
          }
          if (this.menu_name == "extras")
          {
            Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
            if (text == "1")
            {
              if (currentVehicle.Extras(1).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "2")
            {
              if (currentVehicle.Extras(2).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "3")
            {
              if (currentVehicle.Extras(3).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "4")
            {
              if (currentVehicle.Extras(4).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "5")
            {
              if (currentVehicle.Extras(5).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "6")
            {
              if (currentVehicle.Extras(6).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "7")
            {
              if (currentVehicle.Extras(7).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "8")
            {
              if (currentVehicle.Extras(8).Enabled)
                this.DrawBought(e, menuPosX, y);
            }
            else if (text == "9" && currentVehicle.Extras(9).Enabled)
              this.DrawBought(e, menuPosX, y);
          }
          if (text == "Slow")
          {
            Vehicle currentVehicle = this.Player.Character.CurrentVehicle;
            if (!this.veh_brakes_fast.Contains(currentVehicle) && !this.veh_brakes_fastest.Contains(currentVehicle))
              this.DrawBought(e, menuPosX, y);
          }
          else if (text == "Fast")
          {
            if (this.veh_brakes_fast.Contains(this.Player.Character.CurrentVehicle))
              this.DrawBought(e, menuPosX, y);
          }
          else if (text == "Fastest" && this.veh_brakes_fastest.Contains(this.Player.Character.CurrentVehicle))
            this.DrawBought(e, menuPosX, y);
        }
      }
      if (this.show_arrows)
      {
        this.DrawMenuText(e, "", menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        e.Graphics.DrawSprite(this.arrow, new RectangleF(menuPosX + 2.5f, y + 0.18f, 0.3f, 0.3f), Color.FloralWhite);
        y += 0.58f;
      }
      if (!(this.i_info == ""))
      {
        this.DrawMenuText(e, this.i_info, menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        y += 0.58f;
      }
      float num1;
      if (this.menu_name == "menu_pos")
      {
        this.DrawMenuText(e, "Up [W]", menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num2 = y + 0.58f;
        this.DrawMenuText(e, "Down [S]", menuPosX + 2.6f, num2 + 0.26f, menuPosX, num2 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num3 = num2 + 0.58f;
        this.DrawMenuText(e, "Left [A]", menuPosX + 2.6f, num3 + 0.26f, menuPosX, num3 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num4 = num3 + 0.58f;
        this.DrawMenuText(e, "Right [D]", menuPosX + 2.6f, num4 + 0.26f, menuPosX, num4 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        num1 = num4 + 0.58f;
      }
      else if (this.menu_name == "light position")
      {
        this.DrawMenuText(e, "Forward [W]", menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num5 = y + 0.58f;
        this.DrawMenuText(e, "Backward [S]", menuPosX + 2.6f, num5 + 0.26f, menuPosX, num5 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num6 = num5 + 0.58f;
        this.DrawMenuText(e, "Left [A]", menuPosX + 2.6f, num6 + 0.26f, menuPosX, num6 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num7 = num6 + 0.58f;
        this.DrawMenuText(e, "Right [D]", menuPosX + 2.6f, num7 + 0.26f, menuPosX, num7 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num8 = num7 + 0.58f;
        this.DrawMenuText(e, "Down [Z]", menuPosX + 2.6f, num8 + 0.26f, menuPosX, num8 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num9 = num8 + 0.58f;
        this.DrawMenuText(e, "Up [X]", menuPosX + 2.6f, num9 + 0.26f, menuPosX, num9 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        num1 = num9 + 0.58f;
      }
      else if (this.menu_name == "light intensity")
      {
        this.DrawMenuText(e, "- [A]", menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num10 = y + 0.58f;
        this.DrawMenuText(e, "+ [D]", menuPosX + 2.6f, num10 + 0.26f, menuPosX, num10 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        num1 = num10 + 0.58f;
      }
      else
      {
        if (!(this.menu_name == "light color") && !(this.menu_name == "light range"))
          return;
        this.DrawMenuText(e, "< [A]", menuPosX + 2.6f, y + 0.26f, menuPosX, y + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        float num11 = y + 0.58f;
        this.DrawMenuText(e, "> [D]", menuPosX + 2.6f, num11 + 0.26f, menuPosX, num11 + 0.1f, Color.FromArgb(150, 0, 0, 0), Color.White);
        num1 = num11 + 0.58f;
      }
    }
  }
}
