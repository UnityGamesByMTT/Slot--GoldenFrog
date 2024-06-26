#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement
{
    /**
     * a Diffie-Hellman key agreement class.
     * <p>
     * note: This is only the basic algorithm, it doesn't take advantage of
     * long term public keys if they are available. See the DHAgreement class
     * for a "better" implementation.</p>
     */
    public class DHBasicAgreement
        : IBasicAgreement
    {
        private DHPrivateKeyParameters	key;
        private DHParameters			dhParams;

        public virtual void Init(
            ICipherParameters parameters)
        {
            if (parameters is ParametersWithRandom)
            {
                parameters = ((ParametersWithRandom) parameters).Parameters;
            }

            if (!(parameters is DHPrivateKeyParameters))
            {
                throw new ArgumentException("DHEngine expects DHPrivateKeyParameters");
            }

            this.key = (DHPrivateKeyParameters) parameters;
            this.dhParams = key.Parameters;
        }

        public virtual int GetFieldSize()
        {
            return (key.Parameters.P.BitLength + 7) / 8;
        }

        /**
         * given a short term public key from a given party calculate the next
         * message in the agreement sequence.
         */
        public virtual BigInteger CalculateAgreement(
            ICipherParameters pubKey)
        {
            if (this.key == null)
                throw new InvalidOperationException("Agreement algorithm not initialised");

            DHPublicKeyParameters pub = (DHPublicKeyParameters)pubKey;

            if (!pub.Parameters.Equals(dhParams))
                throw new ArgumentException("Diffie-Hellman public key has wrong parameters.");

            BigInteger p = dhParams.P;

            BigInteger peerY = pub.Y;
            if (peerY == null || peerY.CompareTo(BigInteger.One) <= 0 || peerY.CompareTo(p.Subtract(BigInteger.One)) >= 0)
                throw new ArgumentException("Diffie-Hellman public key is weak");

            BigInteger result = peerY.ModPow(key.X, p);
            if (result.Equals(BigInteger.One))
                throw new InvalidOperationException("Shared key can't be 1");

            return result;
        }
    }
}
#pragma warning restore
#endif
