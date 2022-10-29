package no.dunder.mifflin.dundermifflinquotes.service.impl

import no.dunder.mifflin.dundermifflinquotes.auth.AccessTokenPrincipal
import no.dunder.mifflin.dundermifflinquotes.auth.AuthenticationException
import no.dunder.mifflin.dundermifflinquotes.auth.JWTTokenValidator
import no.dunder.mifflin.dundermifflinquotes.service.IAuthenticationService
import org.springframework.stereotype.Service

@Service
class AuthenticationService(private val jwtTokenValidator: JWTTokenValidator) : IAuthenticationService {
    override fun authenticate(authorizationHeader: String): AccessTokenPrincipal {
        if (!authorizationHeader.contains("Bearer")) {
            throw AuthenticationException("Authorization header did not contain Bearer token")
        }

        val jwtToken = authorizationHeader.substring("Bearer ".length)
        return jwtTokenValidator.authenticate(jwtToken)
    }
}