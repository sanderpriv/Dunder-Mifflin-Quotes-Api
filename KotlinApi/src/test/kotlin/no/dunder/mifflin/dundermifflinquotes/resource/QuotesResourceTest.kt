package no.dunder.mifflin.dundermifflinquotes.resource

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import no.dunder.mifflin.dundermifflinquotes.service.IQuotesService
import org.junit.jupiter.api.Test
import org.mockito.BDDMockito.anyInt
import org.mockito.BDDMockito.given
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.boot.test.autoconfigure.json.AutoConfigureJsonTesters
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.boot.test.json.JacksonTester
import org.springframework.boot.test.mock.mockito.MockBean
import org.springframework.http.HttpStatus
import org.springframework.test.web.servlet.MockMvc
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get
import kotlin.random.Random


@SpringBootTest
@AutoConfigureMockMvc
@AutoConfigureJsonTesters
class QuotesResourceTest(@Autowired val mvc: MockMvc, @Autowired val jsonQuote: JacksonTester<Quote>, @Autowired val jsonQuotes: JacksonTester<List<Quote>>) {

    @MockBean
    lateinit var quotesService: IQuotesService

    @Test
    fun getRandomQuote_returnsOKAndRandomQuote() {
        // Arrange
        val quote = getRandomQuote()
        given(quotesService.getRandomQuote()).willReturn(quote)

        // Act
        val endpoint = "/quote"
        val response = mvc.perform(get(endpoint)).andReturn().response

        // Assert
        assert(response.status == HttpStatus.OK.value())
        assert(response.contentAsString == jsonQuote.write(quote).json)
    }

    @Test
    fun getRandomQuote_noQuotes_returnsNull() {
        // Arrange
        given(quotesService.getRandomQuote()).willReturn(null)

        // Act
        val endpoint = "/quote"
        val response = mvc.perform(get(endpoint)).andReturn().response

        // Assert
        assert(response.status == HttpStatus.OK.value())
        assert(response.contentAsString.isEmpty())
    }

    @Test
    fun getQuotes_givenSize_returnsOKAndQuotes() {
        // Arrange
        val quotes = listOf(getRandomQuote(), getRandomQuote(), getRandomQuote())
        given(quotesService.getQuotes(anyInt())).willReturn(quotes)

        // Act
        val endpoint = String.format("/quotes?size=%s", quotes.size)
        val response = mvc.perform(get(endpoint)).andReturn().response

        // Assert
        assert(response.status == HttpStatus.OK.value())
        assert(response.contentAsString == jsonQuotes.write(quotes).json)
    }

    @Test
    fun getQuotes_notGivenSize_returnsBadRequest() {
        // Arrange

        // Act
        val endpoint = "/quotes"
        val response = mvc.perform(get(endpoint)).andReturn().response

        // Assert
        assert(response.status == HttpStatus.BAD_REQUEST.value())
        assert(response.contentAsString.isEmpty())
    }

    private fun getRandomQuote(): Quote {
        return Quote(Random.nextInt(), Random.nextInt(), Random.nextInt(), Random.nextInt(), "line", "speaker", false)
    }
}
