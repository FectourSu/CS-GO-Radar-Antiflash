#include <Windows.h>
#include "MemMan.h"
#include <fstream> 
#include <iostream>
#include <cstring> 
#include <stdlib.h>
#include <conio.h>
#include <string>
#include <ctime>
#include <chrono>
#include <time.h>
#include <map>

#pragma warning(disable : 4996)

using namespace std;

MemMan MemClass;

struct variables
{
	DWORD localPlayer;
	DWORD gameModule;
} val;


int main()
{
	setlocale(LC_ALL, "RUS");

	string resEntity, resSpotted, resDllname, locP, flash;
	ifstream fin("offsets.json");
	fin >> resEntity;
	fin >> resSpotted;
	fin >> locP;
	fin >> flash;
	fin >> resDllname;
	
	int flashDur = 0;
	const char* resDll = resDllname.data();

	DWORD entityList = strtod(resEntity.c_str(), 0);
	DWORD isSpotted = strtod(resSpotted.c_str(), 0);
	DWORD localPlayer = strtod(locP.c_str(), 0);
	DWORD flashDuration = strtod(flash.c_str(), 0);

	int procID = MemClass.getProcess("csgo.exe");
	val.gameModule = MemClass.getModule(procID, resDll);

	try {

		cout << "Cheat activated";
		
		while (true)
		{
			for (short int i = 0; i < 64; i++)
			{
				DWORD entity = MemClass.readMem<DWORD>(val.gameModule + entityList + i * 0x10);
				if (entity != NULL)
					MemClass.writeMem<bool>(entity + isSpotted, true);

				//Anti-Flash
				if (val.localPlayer == NULL)
					while (val.localPlayer == NULL)
						val.localPlayer = MemClass.readMem<DWORD>(val.gameModule + localPlayer);

				flashDur = MemClass.readMem<int>(val.localPlayer + flashDuration);
				if (flashDur > 0)
					MemClass.writeMem<int>(val.localPlayer + flashDuration, 0);
			}

			Sleep(1);

		}
	}
	catch(...)
	{
		cout << "Sorry, error";
	}
	return 0;
}