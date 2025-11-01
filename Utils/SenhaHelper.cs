using System.Security.Cryptography;

namespace Utils;

public static class SenhaHelper
{
    private const int Iterations = 100_000;
    private const int SaltLength = 16;
    private const int KeyBytes = 32;

    /// <summary>
    /// Criptografa senhas SHA256 + Salt
    /// </summary>
    /// <param name="senha">String da senha</param>
    /// <returns>Hash da senha</returns>
    public static string CriptografarSenha(this string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltLength);

        using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(KeyBytes);

        byte[] hashBytes = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);

        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Compara uma hash com uma senha
    /// </summary>
    /// <param name="senha">Senha para comparar</param>
    /// <param name="storedHash">Hash da senha do usuário salva no banco</param>
    /// <returns>true | false</returns>
    public static bool VerificarSenha(this string storedHash, string senha)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        byte[] salt = new byte[SaltLength];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);


        byte[] storedHashBytes = new byte[hashBytes.Length - salt.Length];
        Buffer.BlockCopy(hashBytes, salt.Length, storedHashBytes, 0, storedHashBytes.Length);

        using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations, HashAlgorithmName.SHA256);
        byte[] computedHash = pbkdf2.GetBytes(KeyBytes);

        return CryptographicOperations.FixedTimeEquals(storedHashBytes, computedHash);
    }
}
