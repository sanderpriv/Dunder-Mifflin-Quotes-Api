package no.dunder.mifflin.dundermifflinquotes.resource

import com.auth0.jwt.JWT
import com.auth0.jwt.algorithms.Algorithm
import org.junit.jupiter.api.Test
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.http.HttpStatus
import org.springframework.test.web.servlet.MockMvc
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get

@SpringBootTest
@AutoConfigureMockMvc
class RootResourceTest(@Autowired private val mvc: MockMvc) {

    @Test
    fun root_returnsOK() {
        // Arrange
        val jwtToken = JWT.create()
            .withIssuer("DunderMifflinQuotes")
            .withAudience("UI-DunderMifflinQuotes")
            .withSubject("postman")
            .sign(Algorithm.HMAC256("dunder-mifflin"))

        // Act
        val response = mvc.perform(get("/").header("Authorization", "Bearer $jwtToken")).andReturn().response

        // Assert
        assert(response.status == HttpStatus.OK.value())
        assert(response.contentAsString == "OK")
    }

}
