package no.dunder.mifflin.dundermifflinquotes.service.impl

import no.dunder.mifflin.dundermifflinquotes.service.IRedditService
import org.springframework.stereotype.Service

@Service
class RedditService: IRedditService {
    override fun getCommentsFromLast24Hours(): List<String> {
        return listOf("Hey", "Why?", "Dwight you ignorant slut") //TODO
    }
}