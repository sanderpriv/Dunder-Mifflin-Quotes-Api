package no.dunder.mifflin.dundermifflinquotes.service

import no.dunder.mifflin.dundermifflinquotes.auth.AccessTokenPrincipal

interface IAuthenticationService {
    fun authenticate(authorizationHeader: String): AccessTokenPrincipal
}