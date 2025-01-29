using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    public static class VersaoBase
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax InserirColunasPadrao(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela)
        {
            return tabela
                .WithColumn("Id").AsInt64().PrimaryKey().Identity();
                //.WithColumn("DataCriacao").AsDateTime().NotNullable();

        }
    }
}
