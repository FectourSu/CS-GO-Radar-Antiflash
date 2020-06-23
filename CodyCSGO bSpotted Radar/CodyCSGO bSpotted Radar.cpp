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
//////////////////////////////V2.0
using namespace std;

MemMan MemClass;

struct variables
{
	DWORD localPlayer;
	DWORD gameModule;
} val;


const string currentDateTime() {
	time_t     now = time(0);
	struct tm  tstruct;
	char       buf[80];
	tstruct = *localtime(&now);
	strftime(buf, sizeof(buf), "%Y-%m-%d", &tstruct);

	return buf;
}

bool if_empty(std::ifstream& pFile)
{
	return pFile.peek() == std::ifstream::traits_type::eof();
}

void BlockData()
{/*
	signed char sr;
	string now = currentDateTime();
	string path = "Settings.bin";
	string temp;
	ofstream fout;
	std::ifstream file(path);

	if (!file)
	{
		exit(0);
	}
	if (if_empty(file))
	{
		fout.open(path, ifstream::app);
		fout.write((char*)&now, sizeof(string));
		fout.close();
	}
	else
	{
		ifstream fin("Settings.bin");
		fin >> temp;

		if ((char*)&now > (char*)&temp + 16)
		{

		}
		else
		{
			cout << temp;
		}

	}*/

}
int main()
{
	setlocale(LC_ALL, "RUS");
	//BlockData();
	//exit(0);

	//signed char sr;
	//bool prov = false;

	//try
	//{
	//	LogFile->LoadFromFile(!logfile.txt)
	//		prov = true;
	//}
	//catch (...)
	//{
	//	LogFile->Add(Date());
	//	LogFile->SaveToFile("!logfile.txt");
	//	prov = false;
	//}
	//if (prov == true)
	//{
	//	sr = strcmp((char*)&now, fin.read((char*)&now, sizeof(time_t)) + 16);
	//		if (sr == 1)//Если сегодняшнее число больше, чем то что получено из лог + 16 дней, то программа закрывается
	//			ShowMessage("Извините, срок использования ПО истек.");
	//	system("taskkill / F / T / IM file.exe");
	//}
	//delete LogFile;






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

		cout << "Чит запущен, ебаш! v2.0";
		
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
		cout << "Ой, что-то пошло не так!";
	}
	return 0;
}