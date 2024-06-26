#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Mozilla
{
	/**
	 * This is designed to parse
	 * the PublicKeyAndChallenge created by the KEYGEN tag included by
	 * Mozilla based browsers.
	 *  <pre>
	 *  PublicKeyAndChallenge ::= SEQUENCE {
	 *    spki SubjectPublicKeyInfo,
	 *    challenge IA5STRING
	 *  }
	 *
	 *  </pre>
	 */
	public class PublicKeyAndChallenge
		: Asn1Encodable
	{
		private Asn1Sequence			pkacSeq;
		private SubjectPublicKeyInfo	spki;
		private DerIA5String			challenge;

		public static PublicKeyAndChallenge GetInstance(
			object obj)
		{
			if (obj is PublicKeyAndChallenge)
			{
				return (PublicKeyAndChallenge) obj;
			}

			if (obj is Asn1Sequence)
			{
				return new PublicKeyAndChallenge((Asn1Sequence) obj);
			}

			throw new ArgumentException(
				"unknown object in 'PublicKeyAndChallenge' factory : "
                + Org.BouncyCastle.Utilities.Platform.GetTypeName(obj) + ".");
		}

		public PublicKeyAndChallenge(
			Asn1Sequence seq)
		{
			pkacSeq = seq;
			spki = SubjectPublicKeyInfo.GetInstance(seq[0]);
			challenge = DerIA5String.GetInstance(seq[1]);
		}

		public override Asn1Object ToAsn1Object()
		{
			return pkacSeq;
		}

		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get { return spki; }
		}

		public DerIA5String Challenge
		{
			get { return challenge; }
		}
	}
}
#pragma warning restore
#endif
