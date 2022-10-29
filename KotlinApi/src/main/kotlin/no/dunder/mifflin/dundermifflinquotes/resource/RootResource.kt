package no.dunder.mifflin.dundermifflinquotes.resource

import no.dunder.mifflin.dundermifflinquotes.service.IAuthenticationService
import org.slf4j.LoggerFactory
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestHeader
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController
@RequestMapping("/")
class RootResource(private val authenticationService: IAuthenticationService) {
    private val logger = LoggerFactory.getLogger(RootResource::class.java)

    @GetMapping("/")
    fun root(@RequestHeader("Authorization") authorizationHeader: String): String {
        val tokenPrincipal = authenticationService.authenticate(authorizationHeader)
        logger.info("Got ping from {}", tokenPrincipal.name)

        return "OK"
    }

}