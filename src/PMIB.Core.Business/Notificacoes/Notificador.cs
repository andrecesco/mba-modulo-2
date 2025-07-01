using PMIB.Core.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIB.Core.Business.Notificacoes;

public class Notificador : INotificador
{
    private readonly List<Notificacao> _notificacoes;

    public Notificador()
    {
        _notificacoes = [];
    }

    public void Handle(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<Notificacao> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public bool TemNotificacao()
    {
        return _notificacoes.Count != 0;
    }
}
