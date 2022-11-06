package dunder.mifflin.dundermifflinapp

import androidx.test.rule.ActivityTestRule
import com.adevinta.android.barista.assertion.BaristaVisibilityAssertions.assertDisplayed
import org.junit.Rule
import org.junit.Test

class QuoteFeature: BaseUITest() {

    val activityRule = ActivityTestRule(MainActivity::class.java)
        @Rule get

    @Test
    fun displaysHeader() {
        assertDisplayed("dunder mifflin quotes")
    }
}