package no.dunder.mifflin.dundermifflinquotes.auth

import com.auth0.jwt.JWT
import com.auth0.jwt.JWTVerifier
import com.auth0.jwt.algorithms.Algorithm
import com.auth0.jwt.exceptions.JWTDecodeException
import com.auth0.jwt.impl.JWTParser
import com.auth0.jwt.interfaces.Payload
import org.slf4j.LoggerFactory
import java.util.*

class JWTTokenValidator(
    secretsBySubject: Map<String, String>,
    private val issuer: String,
    private val audience: String) {
    private val logger = LoggerFactory.getLogger(JWTTokenValidator::class.java)
    private val verifiersBySubject = HashMap<String, JWTVerifier>()

    init {
        for (subject in secretsBySubject.keys)
            addJwtVerifier(subject, secretsBySubject[subject]!!)
    }

    fun authenticate(jwtToken: String): AccessTokenPrincipal {
        val payload = jwtToken.parsePayloadWithoutVerification()
        val subject = payload.subject
        val jwtVerifier = verifiersBySubject[subject]

        if (jwtVerifier == null) {
            logger.warn("Cannot find JWT Verifier for subject {}. Throwing authentication exception", subject)
            throw AuthenticationException("Token not valid")
        }

        val decodedJWT = jwtVerifier.verify(jwtToken)
        return AccessTokenPrincipal(decodedJWT.subject)
    }

    private fun addJwtVerifier(subject: String, secret: String) {
        logger.info("Creating JWT verifier for subject: {}", subject)
        val jwtVerifier =
            JWT.require(Algorithm.HMAC256(secret))
            .withIssuer(issuer)
            .withAudience(audience)
            .withSubject(subject).build()
        verifiersBySubject[subject] = jwtVerifier
    }

    private fun String.parsePayloadWithoutVerification() : Payload {
        val base64EncodedClaims = this.getFirstPartOfToken()
        val jsonClaims = String(Base64.getDecoder().decode(base64EncodedClaims))
        return JWTParser().parsePayload(jsonClaims)
    }

    private fun String.getFirstPartOfToken() : String {
        val parts = this.split(".")

        if (this.endsWith(".") && parts.size == 2)
            return parts[0]

        if (parts.size != 3)
            throw JWTDecodeException(String.format("The token was expected to have 3 parts, but got %s.", parts.size))

        return parts[1]
    }

}