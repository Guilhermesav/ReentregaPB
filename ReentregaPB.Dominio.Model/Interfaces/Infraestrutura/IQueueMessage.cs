using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReentregaPB.Dominio.Model.Interfaces.Infraestrutura
{
    public interface IQueueMessage
    {
        Task SendAsync(string messageText, string fila);
    }
}
