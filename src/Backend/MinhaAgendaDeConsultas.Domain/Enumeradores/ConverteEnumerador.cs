using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Enumeradores
{
    public class ConverteEnumerador
    {

        public static string ObterDescricaoEnum(System.Type enumType, System.Enum enumValue)
        {
            // Obtém o membro do enum pelo seu nome
            MemberInfo[] memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                // Tenta obter o atributo Description
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    // Retorna o valor do atributo Description
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            // Caso não tenha o atributo Description, retorna o nome do enum
            return enumValue.ToString();
        }
    }
}
