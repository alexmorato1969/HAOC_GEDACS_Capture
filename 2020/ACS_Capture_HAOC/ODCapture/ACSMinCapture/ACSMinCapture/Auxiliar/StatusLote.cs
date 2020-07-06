using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACSMinCapture.Auxiliar
{
    public enum StatusLote : int    
    {
        Capturando = 1,
        Capturado = 2,
        CapturaCancelada = 3,
        Processando = 4,
        Processado = 5,
        ProcessamentoCancelado = 6
    }
}
