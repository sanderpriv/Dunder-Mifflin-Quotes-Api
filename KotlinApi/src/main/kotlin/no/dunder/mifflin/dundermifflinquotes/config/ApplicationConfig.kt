package no.dunder.mifflin.dundermifflinquotes.config

import no.dunder.mifflin.dundermifflinquotes.auth.JWTTokenValidator
import org.springframework.boot.context.properties.ConfigurationProperties
import org.springframework.boot.context.properties.EnableConfigurationProperties
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration

private const val JWT_ISSUER = "DunderMifflinQuotes"
private const val JWT_AUDIENCE = "UI-DunderMifflinQuotes"

@EnableConfigurationProperties
@Configuration
@ConfigurationProperties
class ApplicationConfig {

    lateinit var jwtSecrets: Map<String, String>

    @Bean
    fun jwtTokenValidator(): JWTTokenValidator = JWTTokenValidator(jwtSecrets, JWT_ISSUER, JWT_AUDIENCE)
}