package no.dunder.mifflin.dundermifflinquotes.auth

import java.security.Principal

data class AccessTokenPrincipal(private val name: String) : Principal {
    override fun getName(): String = name
}