package no.dunder.mifflin.dundermifflinquotes.resource

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import no.dunder.mifflin.dundermifflinquotes.service.IQuotesService
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestParam
import org.springframework.web.bind.annotation.RestController

@RestController
class QuotesResource(private val quotesService: IQuotesService) {

    @GetMapping("/quotes")
    fun getQuotes(@RequestParam size: Int): List<Quote> {
        return quotesService.getQuotes(size)
    }

    @GetMapping("/quote")
    fun getRandomQuote(): Quote? {
        return quotesService.getRandomQuote()
    }

}