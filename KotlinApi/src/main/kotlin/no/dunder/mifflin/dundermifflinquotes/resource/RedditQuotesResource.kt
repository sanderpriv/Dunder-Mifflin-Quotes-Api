package no.dunder.mifflin.dundermifflinquotes.resource

import no.dunder.mifflin.dundermifflinquotes.service.IMatchingService
import no.dunder.mifflin.dundermifflinquotes.service.IQuotesService
import no.dunder.mifflin.dundermifflinquotes.service.IRedditService
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RestController

@RestController
class RedditQuotesResource(
    private val quotesService: IQuotesService,
    private val redditService: IRedditService,
    private val matchingService: IMatchingService
) {

    @GetMapping("/redditQuotes")
    fun getQuotesFromLast24Hours(): Map<String, Int> {
        val quotes = quotesService.getAllQuotes()
        val redditComments = redditService.getCommentsFromLast24Hours()
        val matches = matchingService.getMatchesOfRedditCommentsAndQuotes(redditComments, quotes)
        return matches
    }

}