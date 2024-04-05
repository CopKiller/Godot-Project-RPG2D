namespace EntityFramework.Configuration
{
    public static class DatabaseDirectory
    {
        public static string GetDatabaseDirectory()
        {
            // Obtém o diretório do arquivo de origem do assembly que está sendo executado
            //string diretorioAssembly = Path.GetDirectoryName(Environment.CurrentDirectory);
            // Navega para o diretório pai, que é o diretório raiz do projeto em muitos casos
            string diretorioRaizProjeto = Environment.CurrentDirectory;

            // Navega para cima até chegar ao diretório do banco de dados
            DirectoryInfo diretorio = new(diretorioRaizProjeto);
            var parent = diretorio.Parent;

            // Nome do arquivo de banco de dados
            diretorioRaizProjeto = "Data Source=" + parent + "/DatabaseSqlite" + ".db";

            return diretorioRaizProjeto;
        }

        //public static string GetDatabaseDirectory()
        //{
        //    // Obtém o caminho do diretório do assembly em execução
        //    string diretorioAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //    // Navega para cima até chegar ao diretório do banco de dados
        //    string caminhoBancoDeDados = Path.Combine(diretorioAssembly, "..", "..", "..", "DatabaseSqlite.db");

        //    // Obtém o caminho completo
        //    caminhoBancoDeDados = Path.GetFullPath(caminhoBancoDeDados);

        //    return caminhoBancoDeDados;
        //}
    }
}
