RD /S /q MainApp\obj
RD /S /q lib\ClassLib_Motion\obj
RD /S /q lib\ClassLib_MotionAbs\obj
RD /S /q lib\ClassLib_MotionEdit\obj
RD /S /q lib\ClassLib_MotionStd\obj
RD /S /q lib\ClassLib_MotionUI\obj
RD /S /q lib\ClassLib_MotionView\obj
RD /S /q lib\ClassLib_ParaFile\obj
RD /S /q lib\interface\interface\obj

del MainApp\bin\Debug\*.ilk
del MainApp\bin\Debug\*.pdb
del MainApp\bin\Debug\*.pch
del MainApp\bin\Debug\*.idb
del MainApp\bin\Debug\*.obj

del MainApp\*.sdf
del *.sdf