﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Steering
{
    class Scenario
    {
        static readonly Random random = new Random(DateTime.Now.Millisecond);

        static Vector3 randomPosition(float range)
        {
            Vector3 pos = new Vector3();
            pos.X = (random.Next() % range) - (range / 2);
            pos.Y = (random.Next() % range) - (range / 2);
            pos.Z = (random.Next() % range) - (range / 2);
            return pos;
        }

        public static void setUpFlockingDemo()
        {
            Params.Load("flocking.properties");
            List<Entity> children = XNAGame.Instance().Children;
            //Ground ground = new Ground();
            //children.Add(ground);
            //XNAGame.Instance().Ground = ground;
            Fighter bigFighter = new EliteFighter();
            bigFighter.ModelName = "python";
            bigFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            bigFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wander);
            bigFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.pursuit);
            bigFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.sphere_constrain);
            bigFighter.scale = 10.0f;
            children.Add(bigFighter);        

            float range = Params.GetFloat("world_range");
            Fighter fighter = null;
            for (int i = 0; i < Params.GetFloat("num_boids"); i++)
            {
                Vector3 pos = randomPosition(range);
                
                fighter = new EliteFighter();
                fighter.ModelName = "ferdelance";
                fighter.pos = pos;
                fighter.Target = bigFighter;
                fighter.SteeringBehaviours.turnOffAll();
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.separation);
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.cohesion);
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.alignment);
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wander);
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.sphere_constrain);
                fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
                children.Add(fighter);                
            }

            int numObstacles = 0;
            float dist = (range * 2) / numObstacles;
            for (float x = - range ; x < range ; x+= dist)
            {
                for (float z = - range ; z < range ; z += dist)
                {
                    Obstacle o = new Obstacle(20);
                    o.pos = new Vector3(x, 0, z);
                    o.Color = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                    o.ShouldDraw = true;
                    children.Add(o);
                }
            }

            bigFighter.Target = fighter;

            Fighter camFighter = new EliteFighter();
            Vector3 offset = new Vector3(0, 0, 10);
            fighter.ModelName = "cobramk3";
            camFighter.pos = fighter.pos + offset;
            camFighter.offset = offset;
            camFighter.Leader = fighter;
            camFighter.SteeringBehaviours.turnOffAll();
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.sphere_constrain);
            XNAGame.Instance().Children.Add(camFighter);
    
            XNAGame.Instance().CamFighter = camFighter;
            Camera camera = XNAGame.Instance().Camera;
            camera.pos = new Vector3(0.0f, 60.0f, 200.0f);

            foreach (Entity child in children)
            {
                child.LoadContent();
            }
        }

        public static void setUpStateMachineDemo()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;            
            Ground ground = new Ground();
            children.Add(ground);
            XNAGame.Instance().Ground = ground;            
            AIFighter aiFighter = new AIFighter();
            aiFighter.pos = new Vector3(-20, 50, 50);
            aiFighter.maxSpeed = 16.0f;
            aiFighter.SwicthState(new IdleState(aiFighter));
            aiFighter.Path.DrawPath = true;
            children.Add(aiFighter);

            Fighter fighter = new Fighter();
            fighter.ModelName = "ship2";
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.arrive);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            fighter.pos = new Vector3(10, 50, 0);
            fighter.targetPos = aiFighter.pos + new Vector3(-50, 0, -80);
            children.Add(fighter);

            Fighter camFighter = new Fighter();
            camFighter.Leader = fighter;            
            camFighter.offset = new Vector3(0, 5, 10);
            camFighter.pos = fighter.pos + camFighter.offset;
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            XNAGame.Instance().CamFighter = camFighter;
            children.Add(camFighter);

            XNAGame.Instance().Leader = fighter;
            Camera camera = XNAGame.Instance().Camera;
            camera.pos = new Vector3(0.0f, 60.0f, 100.0f);

            Obstacle o = new Obstacle(4);
            o.pos = new Vector3(0, 50, -10);
            children.Add(o);

            o = new Obstacle(4);
            o.pos = new Vector3(50, 0, -90) + aiFighter.pos;
            children.Add(o);
            foreach (Entity child in children)
            {
                child.LoadContent();
            }
        }

        public static void setUpPursuit()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;

            Ground ground = new Ground();
            children.Add(ground);
            XNAGame.Instance().Ground = ground;            

            Fighter fighter = new Fighter();
            fighter.ModelName = "ship1";
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.arrive);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            fighter.pos = new Vector3(2, 20, -50);
            fighter.targetPos = fighter.pos * 2;
            XNAGame.Instance().Leader = fighter;
            children.Add(fighter);

            Fighter fighter1 = new Fighter();
            fighter1.ModelName = "ship2";
            fighter1.Target = fighter;
            fighter1.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.pursuit);
            fighter1.pos = new Vector3(-20, 20, -20);
            children.Add(fighter1);
            foreach (Entity child in children)
            {
                child.LoadContent();
            }  
        }

        public static void setUpWander()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;
            Fighter leader = new Fighter();
            leader.pos = new Vector3(10, 120, 20);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wander);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            children.Add(leader);

            Fighter camFighter = new Fighter();
            camFighter.Leader = leader;
            camFighter.pos = new Vector3(10, 120, 0);
            camFighter.offset = new Vector3(0, 5, 10);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            XNAGame.Instance().CamFighter = camFighter;
            children.Add(camFighter);

            Ground ground = new Ground();
            children.Add(ground);
            XNAGame.Instance().Ground = ground;

            XNAGame.Instance().Camera.pos = new Vector3(10, 120, 50);
            foreach (Entity child in children)
            {
                child.LoadContent();
            }
      
        }

        public static void setUpArrive()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;
            Fighter leader = new Fighter();
            leader.pos = new Vector3(10, 120, 20);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.arrive);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            leader.targetPos = new Vector3(0, 200, -450);
            children.Add(leader);
            XNAGame.Instance().Leader = leader;

            Fighter camFighter = new Fighter();
            camFighter.Leader = leader;
            camFighter.pos = new Vector3(10, 120, 0);
            camFighter.offset = new Vector3(0, 5, 10);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            XNAGame.Instance().CamFighter = camFighter;
            children.Add(camFighter);

            Ground ground = new Ground();
            children.Add(ground);

            XNAGame.Instance().Ground = ground;
            foreach (Entity child in children)
            {
                child.LoadContent();
            }

        }
        

        public static void setUpBuckRogersDemo()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;
            Fighter leader = new Fighter();
            leader.pos = new Vector3(10, 20, 20);            
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.arrive);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            leader.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            leader.targetPos = new Vector3(0, 100, -450);
            children.Add(leader);
            XNAGame.Instance().Leader = leader;

            // Add some Obstacles

            Obstacle o = new Obstacle(4);
            o.pos = new Vector3(0, 10, -10);
            children.Add(o);

            o = new Obstacle(17);
            o.pos = new Vector3(-10, 16, -80);
            children.Add(o);

            o = new Obstacle(10);
            o.pos = new Vector3(10, 15, -120);
            children.Add(o);

            o = new Obstacle(12);
            o.pos = new Vector3(5, -10, -150);
            children.Add(o);

            o = new Obstacle(20);
            o.pos = new Vector3(-2, 5, -200);
            children.Add(o);

            o = new Obstacle(10);
            o.pos = new Vector3(-25, -20, -250);
            children.Add(o);

            o = new Obstacle(10);
            o.pos = new Vector3(20, -20, -250);
            children.Add(o);

            o = new Obstacle(35);
            o.pos = new Vector3(-10, -30, -300);
            children.Add(o);

            // Now make a fleet
            int fleetSize = 5;
            float xOff = 6;
            float zOff = 6;
            for (int i = 2; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * +zOff;
                    Fighter fleet = new Fighter();
                    fleet.Leader = leader;
                    fleet.offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), 0, z);
                    fleet.pos = leader.pos + fleet.offset;
                    fleet.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
                    fleet.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
                    fleet.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
                    children.Add(fleet);
                }
            }

            Fighter camFighter = new Fighter();
            camFighter.Leader = leader;
            camFighter.pos = new Vector3(0, 15, fleetSize * zOff);
            camFighter.offset = new Vector3(0, 5, fleetSize * zOff);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.wall_avoidance);
            camFighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            XNAGame.Instance().CamFighter = camFighter;
            children.Add(camFighter);


            Ground ground = new Ground();
            children.Add(ground);
            XNAGame.Instance().Ground = ground;
            foreach (Entity child in children)
            {
                child.pos.Y += 100;
                child.LoadContent();
            }
        }

        public static void SetUpMassEffectDemo()
        {
            Params.Load("avoidance.properties");
            List<Entity> children = XNAGame.Instance().Children;
            var positons = new Vector3(300, 100, 200);
            var models = new AIFighter[7];
            var models2 = new AIFighter[7];
            var modelNames = new string[] { "Mass Relay", "Normandy2", "cerberus", "GethDread", "Everest Class Dreadnaught", "AllianceFighter", "Untitled" };
            for (int i = 0; i < models.Count(); i++)
            {
                models[i] = new AIFighter {ModelName = modelNames[i]};
                if (i == 0)
                    models[i].pos = new Vector3(positons.X + 500, positons.Y, positons.Z);
                if (i == 1)
                {
                    models[i].SwicthState(new AllyStates.LeaderIdleState(models[i]));
                }
                if (i == 2)  
                {
                    models[i].pos = new Vector3(120, positons.Y, positons.Z);
                    models[i].SwicthState(new AllyStates.AllieIdleState(models[i]));
                }
                if (i == 3)
                {
                    models[i].pos = new Vector3(20, positons.Y, positons.Z);
                   models[i].SwicthState(new AllyStates.AllieIdleState(models[i]));
                }
                if (i == 4)
                {
                    models[i].pos = new Vector3(-80, positons.Y, positons.Z);
                   models[i].SwicthState(new AllyStates.AllieIdleState(models[i]));
                }
                if (i == 5)
                {
                    models[i].pos = new Vector3(-180, positons.Y, positons.Z);
                    models[i].SwicthState(new AllyStates.AllieIdleState(models[i]));
                }
                if (i == 6)
                {
                    models[i].pos = new Vector3(positons.X -200, positons.Y-1500, positons.Z - 4000);
                }
                children.Add(models[i]);
            }

            positons = new Vector3(-80, 10, -2000);
            models2 = new AIFighter[4];
            modelNames = new string[] { "ReaperSovFBX", "ReaperSovFBX", "ReaperSovFBX", "ReaperSovFBX" };
            for (int i = 0; i < models2.Count(); i++)
            {
                models2[i] = new AIFighter {ModelName = modelNames[i]};
                positons = new Vector3(positons.X + 100, positons.Y, -2000);
                models2[i].look = new Vector3(0, 0, -1);

                models[i] = new AIFighter { ModelName = modelNames[i] };
                if (i == 0)
                {
                    models2[i].pos = new Vector3(120, positons.Y, positons.Z);
                    models2[i].SwicthState(new ReaperStates.ReaperIdleState(models2[i]));
                }
                if (i == 1)
                {
                    models2[i].pos = new Vector3(20, positons.Y, positons.Z);
                    models2[i].SwicthState(new ReaperStates.ReaperIdleState(models2[i]));
                }
                if (i == 2)
                {
                    models2[i].pos = new Vector3(-80, positons.Y, positons.Z);
                    models2[i].SwicthState(new ReaperStates.ReaperIdleState(models2[i]));
                }
                if (i == 3)
                {
                    models2[i].pos = new Vector3(-180, positons.Y, positons.Z);
                    models2[i].SwicthState(new ReaperStates.ReaperIdleState(models2[i]));
                }
                children.Add(models2[i]);
            }

            Video2 v = new Video2();
            XNAGame.Instance().Children.Add(v);


        }
    }
}
