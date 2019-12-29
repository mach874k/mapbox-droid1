﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DroidData {

	private float spawnRate;
	private float catchRate;
	private int attack;
	private int defense;
	private int hp;
	private string crySound;

	public float SpawnRate { get { return spawnRate; } }
	public float CatchRate { get { return catchRate; } }
	public int Attack { get { return attack; } }
	public int Defense { get { return defense; } }
	public int HP { get { return hp; } }
	public string CrySound { get { return crySound; } }

	public DroidData(Droid droid) {
		
		spawnRate = droid.SpawnRate;
		catchRate = droid.CatchRate;
		attack = droid.Attack;
		defense = droid.Defense;
		hp = droid.HP;
		crySound = droid.CrySound.name;

	}

}
