using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace JC.Core.Comuns;

public static class Enumeradores
{
    public static string GetDescription(this Enum GenericEnum) //Hint: Change the method signature and input paramter to use the type parameter T
    {
        Type genericEnumType = GenericEnum.GetType();
        MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
        if ((memberInfo != null && memberInfo.Length > 0))
        {
            var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if ((_Attribs != null && _Attribs.Count() > 0))
            {
                return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
            }
        }
        return GenericEnum.ToString();
    }
}


public static class UploadUtils
{
    public async static Task<string> UploadAsync(IFormFile file)
    {
        string caminhoArquivo = string.Empty;
        try
        {
            if (file.Length > 0)
            {

                string subDiretorio = DateTime.Now.ToString("yyyyMMddHHmmss");
                string diretorio = $"{Constantes.DIRETORIO_IMAGENS}{subDiretorio}\\";

                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                caminhoArquivo = diretorio + file.FileName;

                using (FileStream filestream = File.Create(caminhoArquivo))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Flush();
                }
            }
        }
        catch (Exception ex)
        {
            caminhoArquivo = string.Empty;
        }

        return caminhoArquivo;
    }
}
