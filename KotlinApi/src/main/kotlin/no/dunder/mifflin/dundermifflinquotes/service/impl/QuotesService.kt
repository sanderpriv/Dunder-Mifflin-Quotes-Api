package no.dunder.mifflin.dundermifflinquotes.service.impl

import no.dunder.mifflin.dundermifflinquotes.db.IQuotesRepository
import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import no.dunder.mifflin.dundermifflinquotes.service.IQuotesService
import org.springframework.stereotype.Service
import kotlin.random.Random

@Service
class QuotesService(private val quotesRepository: IQuotesRepository) : IQuotesService {
    override fun getQuotes(size: Int): List<Quote> {
        val orderedQuotes = quotesRepository.getAll()
        val quotes = orderedQuotes.shuffled()

        if (size > quotes.size)
            return quotes

        return quotes.subList(0, size)
    }

    override fun getAllQuotes(): List<Quote> {
        return quotesRepository.getAll()
    }

    override fun getRandomQuote(): Quote? {
        val quotes = quotesRepository.getAll()

        if (quotes.isEmpty())
            return null

        return quotes[Random.nextInt(0, quotes.size)]
    }

}