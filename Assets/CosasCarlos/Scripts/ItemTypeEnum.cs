using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeEnum 
{
    None                        = 0,
    Metal                       = 1,
    MateriaPrima                = 2,
    Manufactura                 = 3,
    Armas                       = 4,
    SuministrosDeBarcos         = 5,
    AlimentosYBebidas           = 6,
    FuentesDeEnergia            = 7,
    MaterialesDeConstrucción    = 8,
    Humanos                     = 9,
    Servicios                   = 10,
    Licencias                   = 11
}

public enum ItemContainerEnum
{
    Barril     = 0,
    Espacio    = 1,
    Cubierta   = 2
}

[Serializable]
public enum ItemStats
{
    MuyBajo  = 1,
    Bajo     = 2,
    Medio    = 3,
    Alto     = 4,
    MuyAlto  = 5
}