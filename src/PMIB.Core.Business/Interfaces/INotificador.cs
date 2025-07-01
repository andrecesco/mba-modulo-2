using PMIB.Core.Business.Notificacoes;

namespace PMIB.Core.Business.Interfaces;
public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}
